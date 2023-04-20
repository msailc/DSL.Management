namespace DSLManagement.Models
{
    public class PipelineStepParameter
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public Guid PipelineStepId { get; set; }
        public PipelineStep PipelineStep { get; set; }
    }
}