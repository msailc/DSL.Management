namespace DSLManagement.Views;

public class UserView
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public List<PipelineView> Pipelines { get; set; }
}