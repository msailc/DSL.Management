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
                var result = await ExecutePipelineStepAsync(step, localPath);

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

        private async Task<PipelineStepResult> ExecutePipelineStepAsync(PipelineStep step, string localPath)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c {step.Command}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = localPath
                }
            };

            // Set the process environment variables
            foreach (var parameter in step.Parameters)
            {
                process.StartInfo.EnvironmentVariables[parameter.Name] = parameter.Value;
            }

            process.Start();

            // Capture the output and wait for the process to exit
            var output = await process.StandardOutput.ReadToEndAsync();
            var error = await process.StandardError.ReadToEndAsync();
            process.WaitForExit();

            // Check if the process exited successfully
            if (process.ExitCode == 0)
            {
                return new PipelineStepResult { Command = step.Command, Parameters = step.Parameters.ToDictionary(p => p.Name, p => p.Value), Success = true };
            }
            else
            {
                return new PipelineStepResult { Command = step.Command, Parameters = step.
                    
                    Parameters.ToDictionary(p => p.Name, p => p.Value), Success = false, ErrorMessage = error };
            }
        }

    }