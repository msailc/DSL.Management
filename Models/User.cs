using Microsoft.AspNetCore.Identity;

namespace DSLManagement.Models

{
    public class User : IdentityUser
    {
        public string Token { get; set; }
        public List<Pipeline> Pipelines { get; set; }
        public List<PipelineExecution> PipelineExecutions { get; set; }
    }
}