using DSLManagement.Models;

public interface IPipelineRepository
{
    Task<Pipeline> GetPipelineAsync(Guid id);
    Task CreatePipelineAsync(Pipeline pipeline);
    Task UpdatePipelineAsync(Pipeline pipeline);
    Task DeletePipelineAsync(Guid id);
}
