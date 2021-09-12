using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChinookASPNETWebAPI.Domain.ApiModels;
using ChinookASPNETWebAPI.Domain.Supervisor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChinookASPNETWebAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;
        private readonly ILogger<PlaylistController> _logger;

        public PlaylistController(IChinookSupervisor chinookSupervisor, ILogger<PlaylistController> logger)
        {
            _chinookSupervisor = chinookSupervisor;
            _logger = logger;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<List<PlaylistApiModel>>> Get()
        {
            return Ok(await _chinookSupervisor.GetAllPlaylist());
        }

        [HttpGet("{id}", Name = "GetPlaylistById")]
        [Produces("application/json")]
        public async Task<ActionResult<PlaylistApiModel>> Get(int id)
        {
            return Ok(await _chinookSupervisor.GetPlaylistById(id));
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<PlaylistApiModel>> Post([FromBody] PlaylistApiModel input)
        {
            return Ok(await _chinookSupervisor.AddPlaylist(input));
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<PlaylistApiModel>> Put(int id, [FromBody] PlaylistApiModel input)
        {
            return Ok(await _chinookSupervisor.UpdatePlaylist(input));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await _chinookSupervisor.DeletePlaylist(id));
        }

        [HttpGet("track/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<List<PlaylistApiModel>>> GetByTrackId(int id)
        {
            return Ok(await _chinookSupervisor.GetPlaylistByTrackId(id));
        }
    }
}