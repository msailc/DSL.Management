using DSLManagement;

public class PipelineRepository : IPipelineRepository
{
    private readonly DataContext _dbContext;

    public PipelineRepository(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Pipeline> GetPipelineAsync(Guid id)
    {
        return await _dbContext.Pipelines.FindAsync(id);
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
}
