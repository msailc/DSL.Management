using DSLManagement.Models;

public class PipelineService : IPipelineService
{
    public async Task<PipelineExecutionResult> ExecutePipelineAsync(Pipeline pipeline)
    {
        if (pipeline == null)
        {
            throw new ArgumentNullException(nameof(pipeline));
        }

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
            var parameters = step.Parameters.ToDictionary(p => p.Name, p => p.Value);
            return new PipelineStepResult { Command = step.Command, Parameters = parameters, Success = true };
        }
        catch (Exception ex)
        {
            var parameters = step.Parameters.ToDictionary(p => p.Name, p => p.Value);
            return new PipelineStepResult { Command = step.Command, Parameters = parameters, Success = false, ErrorMessage = ex.Message };
        }
    }
}