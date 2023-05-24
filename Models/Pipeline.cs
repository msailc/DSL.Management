namespace DSLManagement.Models
{
    public class Pipeline
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<PipelineStep> Steps { get; set; }
        public PipelineStatus Status { get; set; }
        public string UserId { get; set; } 
        public User User { get; set; } 
    }

    public enum PipelineStatus
    {
        Created,
        Running,
        Completed,
        Failed
    }
}
