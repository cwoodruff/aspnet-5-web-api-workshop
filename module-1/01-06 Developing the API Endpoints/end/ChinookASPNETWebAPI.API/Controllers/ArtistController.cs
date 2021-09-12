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
    public class ArtistController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;
        private readonly ILogger<ArtistController> _logger;

        public ArtistController(IChinookSupervisor chinookSupervisor, ILogger<ArtistController> logger)
        {
            _chinookSupervisor = chinookSupervisor;
            _logger = logger;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<List<ArtistApiModel>>> Get()
        {
            return Ok(await _chinookSupervisor.GetAllArtist());
        }

        [HttpGet("{id}", Name = "GetArtistById")]
        [Produces("application/json")]
        public async Task<ActionResult<ArtistApiModel>> Get(int id)
        {
            return Ok(await _chinookSupervisor.GetArtistById(id));
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<ArtistApiModel>> Post([FromBody] ArtistApiModel input)
        {
            return Ok(await _chinookSupervisor.AddArtist(input));
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<ArtistApiModel>> Put(int id, [FromBody] ArtistApiModel input)
        {
            return Ok(await _chinookSupervisor.UpdateArtist(input));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await _chinookSupervisor.DeleteArtist(id));
        }
    }
}