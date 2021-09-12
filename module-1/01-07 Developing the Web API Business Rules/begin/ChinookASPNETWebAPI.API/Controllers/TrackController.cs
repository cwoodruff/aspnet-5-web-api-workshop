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
    public class TrackController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;
        private readonly ILogger<TrackController> _logger;

        public TrackController(IChinookSupervisor chinookSupervisor, ILogger<TrackController> logger)
        {
            _chinookSupervisor = chinookSupervisor;
            _logger = logger;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<List<TrackApiModel>>> Get()
        {
            return Ok(await _chinookSupervisor.GetAllTrack());
        }

        [HttpGet("{id}", Name = "GetTrackById")]
        [Produces("application/json")]
        public async Task<ActionResult<TrackApiModel>> Get(int id)
        {
            return Ok(await _chinookSupervisor.GetTrackById(id));
        }

        [HttpGet("album/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<List<TrackApiModel>>> GetByAlbumId(int id)
        {
            return Ok(await _chinookSupervisor.GetTrackByAlbumId(id));
        }

        [HttpGet("mediatype/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<List<TrackApiModel>>> GetByMediaTypeId(int id)
        {
            return Ok(await _chinookSupervisor.GetTrackByMediaTypeId(id));
        }

        [HttpGet("genre/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<List<TrackApiModel>>> GetByGenreId(int id)
        {
            return Ok(await _chinookSupervisor.GetTrackByGenreId(id));
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<TrackApiModel>> Post([FromBody] TrackApiModel input)
        {
            return Ok(await _chinookSupervisor.AddTrack(input));
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<TrackApiModel>> Put(int id, [FromBody] TrackApiModel input)
        {
            return Ok(await _chinookSupervisor.UpdateTrack(input));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await _chinookSupervisor.DeleteTrack(id));
        }

        [HttpGet("artist/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<List<TrackApiModel>>> GetByArtistId(int id)
        {
            return Ok(await _chinookSupervisor.GetTrackByArtistId(id));
        }

        [HttpGet("invoice/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<List<TrackApiModel>>> GetByInvoiceId(int id)
        {
            return Ok(await _chinookSupervisor.GetTrackByInvoiceId(id));
        }
    }
}