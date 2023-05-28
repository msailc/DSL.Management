using System.Diagnostics;
using System.Text;
using DSLManagement.Hubs;
using DSLManagement.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualBasic.FileIO;

public class PipelineService : IPipelineService
{
    private readonly IPipelineRepository _pipelineRepository;
    private readonly IHubContext<ConsoleHub> _consoleHubContext;

    public PipelineService(IPipelineRepository pipelineRepository, IHubContext<ConsoleHub> consoleHubContext)
    {
        _pipelineRepository = pipelineRepository;
        _consoleHubContext = consoleHubContext;
    }
    
    private async Task LogToConsole(string message)
    {
        await _consoleHubContext.Clients.All.SendAsync("ReceiveMessage", message);
    }

    
    private async void CloneRepository(string gitUrl, string localPath)
        {
            var gitCloneCommand = $"git clone {gitUrl} {localPath}";
            
            await LogToConsole($"Cloning repository from {gitUrl} to {localPath}");
            
            var processInfo = new ProcessStartInfo("cmd.exe", $"/c {gitCloneCommand}");
            Process.Start(processInfo)?.WaitForExit();
        }

    private async void DeleteClonedFolder(string localPath)
    {
        try
        {
            FileSystem.DeleteDirectory(localPath, UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
            await LogToConsole($"Removed repository from {localPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }


    public async Task<PipelineExecutionResult> ExecutePipelineAsync(Guid pipelineId, string gitUrl, bool deleteRepositoryAfterExecution)
    {
        var pipeline = await _pipelineRepository.GetPipelineForExecutionAsync(pipelineId);

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

        var execution = new PipelineExecution
        {
            Id = Guid.NewGuid(),
            PipelineId = pipelineId,
            PipelineName = pipeline.Name,
            Pipeline = pipeline,
            StartTime = DateTime.UtcNow,
            EndTime = null,
            Success = true,
            StepExecutions = new List<PipelineStepExecution>()
        };

        foreach (var step in pipeline.Steps)
        {
            var result = await ExecutePipelineStepAsync(step, localPath);
            execution.StepExecutions.Add(new PipelineStepExecution
            {
                Id = Guid.NewGuid(),
                PipelineStepId = step.Id,
                PipelineStepCommand = step.Command,
                PipelineStep = step,
                StartTime = DateTime.UtcNow,
                EndTime = null,
                Success = result.Success,
                ErrorMessage = result.ErrorMessage
            });

            success = success && result.Success;
            stepResults.Add(result);
        }

        execution.Success = success;
        execution.EndTime = DateTime.UtcNow;

        await _pipelineRepository.SavePipelineExecutionAsync(execution);
        
        if (deleteRepositoryAfterExecution)
        {
            DeleteClonedFolder(localPath);
        }

        return new PipelineExecutionResult { Success = execution.Success, StepResults = stepResults };
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

        var outputBuilder = new StringBuilder();
        var errorBuilder = new StringBuilder();

        process.OutputDataReceived += async (sender, args) =>
        {
            if (!string.IsNullOrEmpty(args.Data))
            {
                outputBuilder.AppendLine(args.Data);
                await LogToConsole(args.Data);
            }
        };

        process.ErrorDataReceived += async (sender, args) =>
        {
            if (!string.IsNullOrEmpty(args.Data))
            {
                errorBuilder.AppendLine(args.Data);
                await LogToConsole(args.Data);
            }
        };

        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        // Wait for the process to exit
        await process.WaitForExitAsync();

        // Check if the process exited successfully
        if (process.ExitCode == 0)
        {
            return new PipelineStepResult { Command = step.Command, Parameters = step.Parameters.ToDictionary(p => p.Name, p => p.Value), Success = true };
        }
        else
        {
            var errorMessage = errorBuilder.ToString();
            var outputMessage = outputBuilder.ToString();
            if (string.IsNullOrEmpty(outputMessage))
            {
                outputMessage = errorMessage;
            }
            return new PipelineStepResult { Command = step.Command, Parameters = step.Parameters.ToDictionary(p => p.Name, p => p.Value), Success = false, ErrorMessage = outputMessage };
        }
    }


    }