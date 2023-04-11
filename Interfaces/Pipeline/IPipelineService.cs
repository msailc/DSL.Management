public interface IPipelineService
{
    Task<PipelineExecutionResult> ExecutePipelineAsync(Pipeline pipeline);
}
