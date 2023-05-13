namespace DSLManagement.Models;

public class PipelineStepExecution
{
    public Guid Id { get; set; }
    public Guid PipelineStepId { get; set; }
    public PipelineStep PipelineStep { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
}
