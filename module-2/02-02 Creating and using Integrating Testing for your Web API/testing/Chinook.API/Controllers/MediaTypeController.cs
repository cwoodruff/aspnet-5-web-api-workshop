using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chinook.Domain.ApiModels;
using Chinook.Domain.Supervisor;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace Chinook.API.Controllers
{
    [Route("api/[controller]")]
    [ResponseCache(Duration = 604800)] // cache for a week
    [EnableCors("CorsPolicy")]
    [ApiController]
    [ApiVersion("1.0")]
    public class MediaTypeController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;
        private readonly ILogger<MediaTypeController> _logger;

        public MediaTypeController(IChinookSupervisor chinookSupervisor, ILogger<MediaTypeController> logger)
        {
            _chinookSupervisor = chinookSupervisor;
            _logger = logger;
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        [SwaggerOperation(
            Summary = "Gets all MediaType",
            Description = "Gets all MediaType",
            OperationId = "MediaType.GetAll",
            Tags = new[] { "MediaTypeEndpoint" })]
        [Produces("application/json")]
        [ResponseCache(Duration = 604800)] // cache for a week
        public async Task<ActionResult<List<MediaTypeApiModel>>> Get()
        {
            try
            {
                return new ObjectResult(await _chinookSupervisor.GetAllMediaType());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{id}", Name = "GetMediaTypeById")]
        [SwaggerOperation(
            Summary = "Gets a specific MediaType",
            Description = "Gets a specific MediaType",
            OperationId = "MediaType.GetOne",
            Tags = new[] { "MediaTypeEndpoint" })]
        [Produces("application/json")]
        public async Task<ActionResult<MediaTypeApiModel>> Get(int id)
        {
            try
            {
                var mediaType = await _chinookSupervisor.GetMediaTypeById(id);

                return Ok(mediaType);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [MapToApiVersion("1.0")]
        [HttpPost]
        [SwaggerOperation(
            Summary = "Creates a new MediaType",
            Description = "Creates a new MediaType",
            OperationId = "MediaType.Create",
            Tags = new[] { "MediaTypeEndpoint" })]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<MediaTypeApiModel>> Post([FromBody] MediaTypeApiModel input)
        {
            try
            {
                if (input == null) return BadRequest("Media Type is null");
                if (!ModelState.IsValid) return BadRequest("Invalid Media Type object");

                var mediaType = await _chinookSupervisor.AddMediaType(input);

                return CreatedAtRoute("GetMediaTypeById", new { id = mediaType.Id }, mediaType);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [MapToApiVersion("1.0")]
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Update an MediaType",
            Description = "Update an MediaType",
            OperationId = "MediaType.Update",
            Tags = new[] { "MediaTypeEndpoint" })]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<MediaTypeApiModel>> Put(int id, [FromBody] MediaTypeApiModel input)
        {
            try
            {
                if (input == null) return BadRequest("Media Type is null");
                if (!ModelState.IsValid) return BadRequest("Invalid Media Type object");

                if (await _chinookSupervisor.UpdateMediaType(input))
                    return CreatedAtRoute("GetMediaTypeById", new { id = input.Id }, input);

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [MapToApiVersion("1.0")]
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete a MediaType",
            Description = "Delete a MediaType",
            OperationId = "MediaType.Delete",
            Tags = new[] { "MediaTypeEndpoint" })]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (await _chinookSupervisor.DeleteMediaType(id)) return Ok();

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}