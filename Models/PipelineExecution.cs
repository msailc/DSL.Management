namespace DSLManagement.Models;

public class PipelineExecution
{
    public Guid Id { get; set; }
    public Guid PipelineId { get; set; }
    public string PipelineName { get; set; }
    public Pipeline Pipeline { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public bool Success { get; set; }
    public List<PipelineStepExecution> StepExecutions { get; set; }
}
