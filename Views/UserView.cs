﻿namespace DSLManagement.Views;

public class UserView
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public int PipelinesCount { get; set; }
    public int SuccessPipelinesCount { get; set; }
    public int FailedPipelinesCount { get; set; }
    public int PipelineStepsCount { get; set; }
    public List<UserPipelineView> Pipelines { get; set; }
}

public class UserPipelineView
{
    public Guid PipelineId { get; set; }
    public string Name { get; set; }
    public List<PipelineStepView> Steps { get; set; }
}

