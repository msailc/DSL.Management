using DSLManagement.Models;
using DSLManagement.Views;

public interface IPipelineRepository
{
    Task<IEnumerable<PipelineView>> GetPipelinesAsync();
    Task<PipelineView> GetPipelineAsync(Guid id);
    Task<Pipeline> GetPipelineForExecutionAsync(Guid id);
    Task CreatePipelineAsync(Pipeline pipeline);
    Task UpdatePipelineAsync(Pipeline pipeline);
    Task DeletePipelineAsync(Guid id);
    Task SavePipelineExecutionAsync(PipelineExecution execution);
    Task<IEnumerable<PipelineExecution>> GetPipelineExecutionListAsync(bool? success);
    Task<PipelineExecutionView> GetPipelineExecutionAsync(Guid id);
}
