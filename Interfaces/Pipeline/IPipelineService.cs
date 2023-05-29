using DSLManagement.Models;

public interface IPipelineService
{
    Task<PipelineExecutionResult> ExecutePipelineAsync(Guid pipelineId, string gitUrl, Guid userId, bool deleteRepositoryAfterExecution);
}
