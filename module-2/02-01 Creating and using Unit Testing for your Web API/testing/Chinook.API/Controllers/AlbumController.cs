using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chinook.Domain.ApiModels;
using Chinook.Domain.Supervisor;
using FluentValidation;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace Chinook.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AlbumController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;
        private readonly ILogger<AlbumController> _logger;

        public AlbumController(IChinookSupervisor chinookSupervisor, ILogger<AlbumController> logger)
        {
            _chinookSupervisor = chinookSupervisor;
            _logger = logger;
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        [SwaggerOperation(
            Summary = "Gets all Album",
            Description = "Gets all Album",
            OperationId = "Album.GetAll",
            Tags = new[] { "AlbumEndpoint" })]
        [Produces(typeof(List<AlbumApiModel>))]
        public async Task<ActionResult<List<AlbumApiModel>>> Get()
        {
            try
            {
                return new ObjectResult(await _chinookSupervisor.GetAllAlbum());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{id}", Name = "GetAlbumById")]
        [SwaggerOperation(
            Summary = "Gets a specific Album",
            Description = "Gets a specific Album",
            OperationId = "Album.GetOne",
            Tags = new[] { "AlbumEndpoint" })]
        [Produces("application/json")]
        public async Task<ActionResult<AlbumApiModel>> Get(int id)
        {
            try
            {
                var album = await _chinookSupervisor.GetAlbumById(id);
                if (album == null) return NotFound();

                return Ok(album);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController GetById action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [MapToApiVersion("1.0")]
        [HttpGet("artist/{id}", Name = "GetByArtistId")]
        [SwaggerOperation(
            Summary = "Gets Albums by Artist",
            Description = "Gets Albums by Artist",
            OperationId = "Album.GetByArtist",
            Tags = new[] { "AlbumEndpoint" })]
        [Produces("application/json")]
        public async Task<ActionResult<List<AlbumApiModel>>> GetByArtistId(int id)
        {
            try
            {
                var artist = await _chinookSupervisor.GetArtistById(id);

                return Ok(await _chinookSupervisor.GetAlbumByArtistId(id));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController GetByArtistId action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [MapToApiVersion("1.0")]
        [HttpPost]
        [SwaggerOperation(
            Summary = "Creates a new Album",
            Description = "Creates a new Album",
            OperationId = "Album.Create",
            Tags = new[] { "AlbumEndpoint" })]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<AlbumApiModel>> Post([FromBody] AlbumApiModel input)
        {
            try
            {
                if (input == null) return BadRequest("Album is null");
                if (!ModelState.IsValid) return BadRequest("Invalid Album object");

                var album = await _chinookSupervisor.AddAlbum(input);

                return CreatedAtRoute("GetAlbumById", new { id = album.Id }, album);
            }
            catch (ValidationException ex)
            {
                _logger.LogError($"Validation error for Album Post action: {ex}");
                return StatusCode(422, "Validation error for Album");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Post action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [MapToApiVersion("1.0")]
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Update an Album",
            Description = "Update an Album",
            OperationId = "Album.Update",
            Tags = new[] { "AlbumEndpoint" })]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<AlbumApiModel>> Put(int id, [FromBody] AlbumApiModel input)
        {
            try
            {
                if (input == null) return BadRequest("Album is null");
                if (!ModelState.IsValid) return BadRequest("Invalid Album object");
                if (await Task.Run(() => _chinookSupervisor.GetAlbumById(id)) == null) return NotFound();
                if (await Task.Run(() => _chinookSupervisor.UpdateAlbum(input)))
                    return CreatedAtRoute("GetAlbumById", new { id = input.Id }, input);

                return StatusCode(500);
            }
            catch (ValidationException ex)
            {
                _logger.LogError($"Validation error for Album Post action: {ex}");
                return StatusCode(422, "Validation error for Album");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Put action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [MapToApiVersion("1.0")]
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete a Album",
            Description = "Delete a Album",
            OperationId = "Album.Delete",
            Tags = new[] { "AlbumEndpoint" })]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (await _chinookSupervisor.GetAlbumById(id) == null) return NotFound();

                if (await _chinookSupervisor.DeleteAlbum(id)) return Ok();

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Delete action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}