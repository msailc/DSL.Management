namespace DSLManagement.Models;

public class PipelineExecution
{
    public Guid Id { get; set; }
    public Guid PipelineId { get; set; }
    public Pipeline Pipeline { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public bool Success { get; set; }
    public List<PipelineStepExecution> StepExecutions { get; set; }
    public string Output { get; set; }
    public string Error { get; set; }
}
