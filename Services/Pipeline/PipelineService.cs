using System.Diagnostics;
using DSLManagement.Models;

public class PipelineService : IPipelineService
{
    private readonly IPipelineRepository _pipelineRepository;
        
    public PipelineService(IPipelineRepository pipelineRepository)
    {
        _pipelineRepository = pipelineRepository;
    }
    private void CloneRepository(string gitUrl, string localPath)
        {
            // Construct the Git clone command
            var gitCloneCommand = $"git clone {gitUrl} {localPath}";

            // Use Git command to clone the repository locally
            var processInfo = new ProcessStartInfo("cmd.exe", $"/c {gitCloneCommand}");
            Process.Start(processInfo)?.WaitForExit();
        }

        public async Task<PipelineExecutionResult> ExecutePipelineAsync(Guid pipelineId, string gitUrl)
        {
            var pipeline = await _pipelineRepository.GetPipelineAsync(pipelineId);

            if (pipeline == null)
            {
                throw new ArgumentException($"Pipeline with ID {pipelineId} not found.");
            }

            // Generate a local path for the repository
            var localPath = $"C:\\Repositories\\{Guid.NewGuid()}";

            // Clone the repository to the local path
            CloneRepository(gitUrl, localPath);

            var stepResults = new List<PipelineStepResult>();
            bool success = true;

            foreach (var step in pipeline.Steps)
            {
                var result = await ExecutePipelineStepAsync(step);

                stepResults.Add(result);

                if (!result.Success)
                {
                    success = false;
                    break;
                }
            }

            // Delete the local repository directory
            // Directory.Delete(localPath, true);

            return new PipelineExecutionResult { Success = success, StepResults = stepResults };
        }

        private async Task<PipelineStepResult> ExecutePipelineStepAsync(PipelineStep step)
        {
            try
            {
                var parameters = step.Parameters.ToDictionary(p => p.Name, p => p.Value);
                return new PipelineStepResult { Command = step.Command, Parameters = parameters, Success = true };
            }
            catch (Exception ex)
            {
                var parameters = step.Parameters.ToDictionary(p => p.Name, p => p.Value);
                return new PipelineStepResult { Command = step.Command, Parameters = parameters, Success = false, ErrorMessage = ex.Message };
            }
        }
    }