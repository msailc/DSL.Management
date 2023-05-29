public class PipelineExecutionResult
{
    public bool Success { get; set; }
    public List<PipelineStepResult> StepResults { get; set; }
}

public class PipelineStepResult
{
    public string Command { get; set; }
    public Dictionary<string, string> Parameters { get; set; }
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
}

public class PipelineExecutionRequest
{
    public string GitUrl { get; set; }
    public Guid UserId { get; set; }
}
