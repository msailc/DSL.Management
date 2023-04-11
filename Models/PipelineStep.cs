using System.ComponentModel.DataAnnotations.Schema;

public class PipelineStep
{
    public Guid Id { get; set; }
    public string Command { get; set; }
    [NotMapped]
    public Dictionary<string, string> Parameters { get; set; }
}