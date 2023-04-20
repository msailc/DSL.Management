using DSLManagement.Models;

public interface IPipelineService
{
    Task<PipelineExecutionResult> ExecutePipelineAsync(Pipeline pipeline);
}
