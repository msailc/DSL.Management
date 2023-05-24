using DSLManagement;
using DSLManagement.Models;
using Microsoft.EntityFrameworkCore;

public class PipelineRepository : IPipelineRepository
{
    private readonly DataContext _dbContext;

    public PipelineRepository(DataContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<Pipeline>> GetPipelinesAsync()
    {
        return await _dbContext.Pipelines
            .Include(p => p.Steps)
            .ToListAsync();
    }

    public async Task<Pipeline> GetPipelineAsync(Guid id)
    {
        return await _dbContext.Pipelines
            .Include(p => p.Steps)
            .ThenInclude(s => s.Parameters)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task CreatePipelineAsync(Pipeline pipeline)
    {
        await _dbContext.Pipelines.AddAsync(pipeline);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdatePipelineAsync(Pipeline pipeline)
    {
        _dbContext.Pipelines.Update(pipeline);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeletePipelineAsync(Guid id)
    {
        var pipeline = await GetPipelineAsync(id);
        if (pipeline != null)
        {
            _dbContext.Pipelines.Remove(pipeline);
            await _dbContext.SaveChangesAsync();
        }
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
