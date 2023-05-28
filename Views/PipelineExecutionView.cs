namespace DSLManagement.Views;

public class PipelineExecutionView
{
    public Guid Id { get; set; }
    public Guid PipelineId { get; set; }
    public string PipelineName { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public bool Success { get; set; }
    public List<PipelineStepExecutionView> StepExecutions { get; set; }
}

public class PipelineExecutionSummaryView
{
    public Guid Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public bool Success { get; set; }
}

public class PipelineStepExecutionView
{
    public Guid Id { get; set; }
    public Guid PipelineStepId { get; set; }
    public string PipelineStepCommand { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
}