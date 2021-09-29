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
    [EnableCors("CorsPolicy")]
    [ApiController]
    [ResponseCache(Duration = 604800)]
    [ApiVersion("1.0")]
    public class GenreController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;
        private readonly ILogger<GenreController> _logger;

        public GenreController(IChinookSupervisor chinookSupervisor, ILogger<GenreController> logger)
        {
            _chinookSupervisor = chinookSupervisor;
            _logger = logger;
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        [SwaggerOperation(
            Summary = "Gets all Genre",
            Description = "Gets all Genre",
            OperationId = "Genre.GetAll",
            Tags = new[] { "GenreEndpoint" })]
        [Produces("application/json")]
        public async Task<ActionResult<List<GenreApiModel>>> Get()
        {
            try
            {
                return new ObjectResult(await _chinookSupervisor.GetAllGenre());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{id}", Name = "GetGenreById")]
        [SwaggerOperation(
            Summary = "Gets a specific Genre",
            Description = "Gets a specific Genre",
            OperationId = "Genre.GetOne",
            Tags = new[] { "GenreEndpoint" })]
        [Produces("application/json")]
        public async Task<ActionResult<GenreApiModel>> Get(int id)
        {
            try
            {
                var genre = await _chinookSupervisor.GetGenreById(id);

                return Ok(genre);
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
            Summary = "Creates a new Genre",
            Description = "Creates a new Genre",
            OperationId = "Genre.Create",
            Tags = new[] { "GenreEndpoint" })]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<GenreApiModel>> Post([FromBody] GenreApiModel input)
        {
            try
            {
                if (input == null) return BadRequest("Genre is null");
                if (!ModelState.IsValid) return BadRequest("Invalid Genre object");

                var genre = await _chinookSupervisor.AddGenre(input);

                return CreatedAtRoute("GetGenreById", new { id = genre.Id }, genre);
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
            Summary = "Update an Genre",
            Description = "Update an Genre",
            OperationId = "Genre.Update",
            Tags = new[] { "GenreEndpoint" })]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<GenreApiModel>> Put(int id, [FromBody] GenreApiModel input)
        {
            try
            {
                if (input == null) return BadRequest("Genre is null");
                if (!ModelState.IsValid) return BadRequest("Invalid Genre object");

                if (await _chinookSupervisor.UpdateGenre(input))
                    return CreatedAtRoute("GetGenreById", new { id = input.Id }, input);

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
            Summary = "Delete a Genre",
            Description = "Delete a Genre",
            OperationId = "Genre.Delete",
            Tags = new[] { "GenreEndpoint" })]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (await _chinookSupervisor.DeleteGenre(id)) return Ok();

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