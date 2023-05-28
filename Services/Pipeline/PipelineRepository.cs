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
            .Include(p => p.LastExecutions)
            .ThenInclude(c => c.CommitTitles)
            .FirstOrDefaultAsync(p => p.Id == id);

        var lastExecutions = pipeline.LastExecutions
            .OrderByDescending(e => e.StartTime)
            .Take(5);

        var pipelineView = new PipelineView
        {
            Id = pipeline.Id,
            Name = pipeline.Name,
            UserId = pipeline.UserId,
            LastExecutions = lastExecutions.Select(e => new PipelineExecutionSummaryView
            {
                Id = e.Id,
                StartTime = e.StartTime,
                EndTime = e.EndTime,
                Success = e.Success,
                CommitTitles = e.CommitTitles.Select(c => c.Title).ToList()
            }).ToList(),
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

    public async Task<PipelineExecutionView> GetPipelineExecutionAsync(Guid id)
    {
        var pipelineExecution = await _dbContext.PipelineExecutions
            .Include(e => e.StepExecutions)
            .ThenInclude(s => s.PipelineStep)
            .ThenInclude(p => p.Parameters)
            .Include(e => e.CommitTitles)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (pipelineExecution == null)
        {
            return null; // Or handle the case where the pipeline execution with the given id is not found
        }

        var pipelineExecutionView = new PipelineExecutionView
        {
            Id = pipelineExecution.Id,
            PipelineId = pipelineExecution.PipelineId,
            PipelineName = pipelineExecution.Pipeline?.Name, // Add null check for the pipeline object
            StartTime = pipelineExecution.StartTime,
            EndTime = pipelineExecution.EndTime,
            Success = pipelineExecution.Success,
            CommitTitles = pipelineExecution.CommitTitles?.Select(c => c.Title).ToList(),
            StepExecutions = pipelineExecution.StepExecutions?.Select(s => new PipelineStepExecutionView
            {
                Id = s.Id,
                PipelineStepId = s.PipelineStepId,
                PipelineStepCommand = s.PipelineStep?.Command, // Add null check for the PipelineStep object
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                Success = s.Success,
                ErrorMessage = s.ErrorMessage
            }).ToList()
        };

        return pipelineExecutionView;
    }

}
    
    