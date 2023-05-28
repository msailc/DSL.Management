using System;
using System.Threading.Tasks;
using DSLManagement.Models;
using DSLManagement.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DSLManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PipelineController : ControllerBase
    {
        private readonly ILogger<PipelineController> _logger;
        private readonly IPipelineRepository _pipelineRepository;
        private readonly IPipelineService _pipelineService;

        public PipelineController(ILogger<PipelineController> logger, IPipelineRepository pipelineRepository, IPipelineService pipelineService)
        {
            _logger = logger;
            _pipelineRepository = pipelineRepository;
            _pipelineService = pipelineService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPipelines()
        {
                var pipelines = await _pipelineRepository.GetPipelinesAsync();
                return Ok(pipelines);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<PipelineView>> GetPipeline(Guid id)
        {
            var pipeline = await _pipelineRepository.GetPipelineAsync(id);

            if (pipeline == null)
            {
                return NotFound();
            }

            return pipeline;
        }

        [HttpPost]
        public async Task<ActionResult<Pipeline>> CreatePipeline(Pipeline pipeline)
        {
            await _pipelineRepository.CreatePipelineAsync(pipeline);

            return CreatedAtAction(nameof(GetPipeline), new { id = pipeline.Id }, pipeline);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePipeline(Guid id, Pipeline pipeline)
        {
            if (id != pipeline.Id)
            {
                return BadRequest();
            }

            await _pipelineRepository.UpdatePipelineAsync(pipeline);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePipeline(Guid id)
        {
            await _pipelineRepository.DeletePipelineAsync(id);

            return NoContent();
        }

        [HttpPost("{pipelineId}/execute")]
        public async Task<ActionResult<PipelineExecutionResult>> ExecutePipeline(Guid pipelineId, [FromBody] PipelineExecutionRequest request, bool deleteRepositoryAfterExecution = false)
        {
            var result = await _pipelineService.ExecutePipelineAsync(pipelineId, request.GitUrl, deleteRepositoryAfterExecution);

            return result;
        }

        
        [HttpGet("executions")]
        public async Task<IActionResult> GetPipelineExecutions([FromQuery] bool? success)
        {
            var executions = await _pipelineRepository.GetPipelineExecutionListAsync(success);
            return Ok(executions);
        }

        [HttpGet("executions/{id}")]
        public async Task<ActionResult<PipelineExecutionView>> GetPipelineExecution(Guid id)
        {
            var execution = await _pipelineRepository.GetPipelineExecutionAsync(id);
            if (execution == null)
            {
                return NotFound();
            }
            return execution;
        }

    }
}
