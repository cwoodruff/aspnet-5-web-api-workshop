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
    [ApiVersion("1.0")]
    public class PlaylistController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;
        private readonly ILogger<PlaylistController> _logger;

        public PlaylistController(IChinookSupervisor chinookSupervisor, ILogger<PlaylistController> logger)
        {
            _chinookSupervisor = chinookSupervisor;
            _logger = logger;
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        [SwaggerOperation(
            Summary = "Gets all Playlist",
            Description = "Gets all Playlist",
            OperationId = "Playlist.GetAll",
            Tags = new[] { "PlaylistEndpoint" })]
        [Produces("application/json")]
        public async Task<ActionResult<List<PlaylistApiModel>>> Get()
        {
            try
            {
                return new ObjectResult(await _chinookSupervisor.GetAllPlaylist());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{id}", Name = "GetPlaylistById")]
        [SwaggerOperation(
            Summary = "Gets a specific Playlist",
            Description = "Gets a specific Playlist",
            OperationId = "Playlist.GetOne",
            Tags = new[] { "PlaylistEndpoint" })]
        [Produces("application/json")]
        public async Task<ActionResult<PlaylistApiModel>> Get(int id)
        {
            try
            {
                var playList = await _chinookSupervisor.GetPlaylistById(id);

                return Ok(playList);
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
            Summary = "Creates a new Playlist",
            Description = "Creates a new Playlist",
            OperationId = "Playlist.Create",
            Tags = new[] { "PlaylistEndpoint" })]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<PlaylistApiModel>> Post([FromBody] PlaylistApiModel input)
        {
            try
            {
                if (input == null) return BadRequest("Playlist is null");
                if (!ModelState.IsValid) return BadRequest("Invalid Playlist object");

                var playlist = await _chinookSupervisor.AddPlaylist(input);

                return CreatedAtRoute("GetPlaylistById", new { id = playlist.Id }, playlist);
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
            Summary = "Update an Playlist",
            Description = "Update an Playlist",
            OperationId = "Playlist.Update",
            Tags = new[] { "PlaylistEndpoint" })]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<PlaylistApiModel>> Put(int id, [FromBody] PlaylistApiModel input)
        {
            try
            {
                if (input == null) return BadRequest("Playlist is null");
                if (!ModelState.IsValid) return BadRequest("Invalid Playlist object");

                if (await _chinookSupervisor.UpdatePlaylist(input))
                    return CreatedAtRoute("GetPlaylistById", new { id = input.Id }, input);

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
            Summary = "Delete a Playlist",
            Description = "Delete a Playlist",
            OperationId = "Playlist.Delete",
            Tags = new[] { "PlaylistEndpoint" })]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (await _chinookSupervisor.DeletePlaylist(id)) return Ok();

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [MapToApiVersion("1.0")]
        [HttpGet("track/{id}")]
        [SwaggerOperation(
            Summary = "Gets Playlist by Track",
            Description = "Gets Playlist by Track",
            OperationId = "Playlist.GetByTrackId",
            Tags = new[] { "PlaylistEndpoint" })]
        [Produces("application/json")]
        public async Task<ActionResult<TrackApiModel>> GetByTrackId(int id)
        {
            try
            {
                return Ok(await _chinookSupervisor.GetPlaylistByTrackId(id));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}