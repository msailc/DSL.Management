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
    public List<CommitTitle> CommitTitles { get; set; }
}

public class CommitTitle
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Guid PipelineExecutionId { get; set; }
    public PipelineExecution PipelineExecution { get; set; }
}
