namespace DSLManagement.Views;

public class PipelineView
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public List<PipelineExecutionSummaryView> LastExecutions { get; set; }
    public List<PipelineStepView> Steps { get; set; }
}

public class PipelineStepView
{
    public Guid StepId { get; set; }
    public string Command { get; set; }
    public List<PipelineStepParameterView> Parameters { get; set; }
}

public class PipelineStepParameterView
{
    public Guid ParameterId { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
}