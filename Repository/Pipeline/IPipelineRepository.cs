public interface IPipelineRepository
{
    Task<Pipeline> GetPipelineAsync(Guid pipelineId);
    Task CreatePipelineAsync(Pipeline pipeline);
    Task UpdatePipelineAsync(Pipeline pipeline);
    Task DeletePipelineAsync(Guid pipelineId);
}