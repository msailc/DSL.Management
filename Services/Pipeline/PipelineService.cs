public class PipelineService : IPipelineService
{
    public async Task<PipelineExecutionResult> ExecutePipelineAsync(Pipeline pipeline)
    {
        var stepResults = new List<PipelineStepResult>();
        bool success = true;

        foreach (var step in pipeline.Steps)
        {
            var result = await ExecutePipelineStepAsync(step);

            stepResults.Add(result);

            if (!result.Success)
            {
                success = false;
                break;
            }
        }

        return new PipelineExecutionResult { Success = success, StepResults = stepResults };
    }

    private async Task<PipelineStepResult> ExecutePipelineStepAsync(PipelineStep step)
    {
        try
        {
            return new PipelineStepResult { Command = step.Command, Parameters = step.Parameters, Success = true };
        }
        catch (Exception ex)
        {
            return new PipelineStepResult { Command = step.Command, Parameters = step.Parameters, Success = false, ErrorMessage = ex.Message };
        }
    }
}
