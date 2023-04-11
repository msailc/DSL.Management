public class Pipeline
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<PipelineStep> Steps { get; set; }
    public PipelineStatus Status { get; set; }
}

public enum PipelineStatus
{
    Created,
    Running,
    Completed,
    Failed
}
