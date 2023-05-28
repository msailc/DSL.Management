

using Newtonsoft.Json;

namespace DSLManagement.Models
{
    public class PipelineStep
    {
        public Guid Id { get; set; }
        public string Command { get; set; }
        public List<PipelineStepParameter> Parameters { get; set; }
        public Guid PipelineId { get; set; }
        [JsonIgnore]
        public Pipeline Pipeline { get; set; }
    }
}