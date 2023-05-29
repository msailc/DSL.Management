
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DSLManagement.Hubs
{
    public class ConsoleHub : Hub
    {
        private readonly IPipelineService _pipelineService;

        public ConsoleHub(IPipelineService pipelineService)
        {
            _pipelineService = pipelineService;
        }

        public async Task Execute(string gitUrl, Guid pipelineId,Guid userId, bool deleteRepositoryAfterExecution = false)
        {
            var pipelineExecution = await _pipelineService.ExecutePipelineAsync(pipelineId, gitUrl, userId, deleteRepositoryAfterExecution);

            foreach (var stepResult in pipelineExecution.StepResults)
            {
                var outputLine = $"Executing {stepResult.Command}";

                if (stepResult.Success)
                {
                    outputLine += " - Success";
                }
                else
                {
                    outputLine += $" - Failed: {stepResult.ErrorMessage}";
                }

                await Clients.All.SendAsync("ReceiveMessage", outputLine);
            }
        }
    }
}
