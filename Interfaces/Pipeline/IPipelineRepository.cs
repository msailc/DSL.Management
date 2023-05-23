using DSLManagement.Models;

public interface IPipelineRepository
{
    Task<IEnumerable<Pipeline>> GetPipelinesAsync();
    Task<Pipeline> GetPipelineAsync(Guid id);
    Task CreatePipelineAsync(Pipeline pipeline);
    Task UpdatePipelineAsync(Pipeline pipeline);
    Task DeletePipelineAsync(Guid id);
    Task SavePipelineExecutionAsync(PipelineExecution execution);
    
}
