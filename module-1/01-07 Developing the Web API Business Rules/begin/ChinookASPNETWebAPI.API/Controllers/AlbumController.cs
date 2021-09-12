using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ChinookASPNETWebAPI.Domain.ApiModels;
using ChinookASPNETWebAPI.Domain.Supervisor;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChinookASPNETWebAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;
        private readonly ILogger<AlbumController> _logger;

        public AlbumController(IChinookSupervisor chinookSupervisor, ILogger<AlbumController> logger)
        {
            _chinookSupervisor = chinookSupervisor;
            _logger = logger;
        }

        [HttpGet]
        [Produces(typeof(List<AlbumApiModel>))]
        public async Task<ActionResult<List<AlbumApiModel>>> Get()
        {
            return Ok(await _chinookSupervisor.GetAllAlbum());
        }

        [HttpGet("{id}", Name = "GetAlbumById")]
        public async Task<ActionResult<AlbumApiModel>> Get(int id)
        {

            return Ok(await _chinookSupervisor.GetAlbumById(id));
        }

        [HttpGet("artist/{id}", Name = "GetByArtistId")]
        public async Task<ActionResult<List<AlbumApiModel>>> GetByArtistId(int id)
        {
            return Ok(await _chinookSupervisor.GetAlbumByArtistId(id));
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<AlbumApiModel>> Post([FromBody] AlbumApiModel input)
        {

            return Ok(await _chinookSupervisor.AddAlbum(input));
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<AlbumApiModel>> Put(int id, [FromBody] AlbumApiModel input)
        {
            return Ok(await _chinookSupervisor.UpdateAlbum(input));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await _chinookSupervisor.DeleteAlbum(id));
        }
    }
}