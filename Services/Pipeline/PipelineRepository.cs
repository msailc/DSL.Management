using DSLManagement;
using DSLManagement.Models;
using DSLManagement.Views;
using Microsoft.EntityFrameworkCore;

public class PipelineRepository : IPipelineRepository
{
    private readonly DataContext _dbContext;

    public PipelineRepository(DataContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<PipelineView>> GetPipelinesAsync()
    {
        return await _dbContext.Pipelines
            .Include(p => p.Steps)
            .Select(p => new PipelineView
            {
                Id = p.Id,
                Name = p.Name,
                Steps = p.Steps.Select(s => new PipelineStepView
                {
                    StepId = s.Id,
                    Command = s.Command,
                    Parameters = s.Parameters.Select(p => new PipelineStepParameterView
                    {
                        ParameterId = p.Id,
                        Name = p.Name,
                        Value = p.Value
                    }).ToList()
                }).ToList()
            })
            .ToListAsync<PipelineView>();
    }
    
    // This method is used to retrieve a pipeline for execution. It includes the steps and parameters needed for execution.
    public async Task<Pipeline> GetPipelineForExecutionAsync(Guid id)
    {
        return await _dbContext.Pipelines
            .Include(p => p.Steps)
            .ThenInclude(s => s.Parameters)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<PipelineView> GetPipelineAsync(Guid id)
    {
        var pipeline = await _dbContext.Pipelines
            .Include(p => p.Steps)
            .ThenInclude(pr => pr.Parameters)
            .FirstOrDefaultAsync(p => p.Id == id);

        var pipelineView = new PipelineView
        {
            Id = pipeline.Id,
            Name = pipeline.Name,
            UserId = pipeline.UserId,
            Steps = pipeline.Steps?.Select(s => new PipelineStepView
            {
                StepId = s.Id,
                Command = s.Command,
                Parameters = s.Parameters?.Select(pr => new PipelineStepParameterView
                {
                    ParameterId = pr.Id,
                    Name = pr.Name,
                    Value = pr.Value
                }).ToList()
            }).ToList()
        };

        return pipelineView;
    }
    
    public async Task CreatePipelineAsync(Pipeline pipeline)
    {
        _dbContext.Pipelines.Add(pipeline);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task UpdatePipelineAsync(Pipeline pipeline)
    {
        _dbContext.Pipelines.Update(pipeline);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeletePipelineAsync(Guid id)
    {
        var pipeline = await _dbContext.Pipelines.FirstOrDefaultAsync(p => p.Id == id);
        _dbContext.Pipelines.Remove(pipeline);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task SavePipelineExecutionAsync(PipelineExecution execution)
    {
        _dbContext.PipelineExecutions.Add(execution);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<PipelineExecution>> GetPipelineExecutionListAsync(bool? success)
    {
        IQueryable<PipelineExecution> query = _dbContext.PipelineExecutions;

        if (success.HasValue)
        {
            query = query.Include(e => e.StepExecutions).ThenInclude(s => s.PipelineStep)
                .Where(e => e.Success == success.Value);
        }
        else
        {
            query = query.Include(e => e.StepExecutions).ThenInclude(s => s.PipelineStep);
        }

        return await query.ToListAsync();
    }


}
