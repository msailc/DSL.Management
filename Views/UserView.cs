namespace DSLManagement.Views;

public class UserView
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public List<UserPipelineView> Pipelines { get; set; }
}

public class UserPipelineView
{
    public Guid PipelineId { get; set; }
    public string Name { get; set; }
}