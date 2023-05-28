using Microsoft.AspNetCore.Identity;

namespace DSLManagement.Models

{
    public class User : IdentityUser
    {
        public string Token { get; set; }
        public List<Pipeline> Pipelines { get; set; }
    }
}