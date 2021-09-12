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
    [ResponseCache(Duration = 604800)]
    public class GenreController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;
        private readonly ILogger<GenreController> _logger;

        public GenreController(IChinookSupervisor chinookSupervisor, ILogger<GenreController> logger)
        {
            _chinookSupervisor = chinookSupervisor;
            _logger = logger;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<List<GenreApiModel>>> Get()
        {
            return Ok(await _chinookSupervisor.GetAllGenre());
        }

        [HttpGet("{id}", Name = "GetGenreById")]
        [Produces("application/json")]
        public async Task<ActionResult<GenreApiModel>> Get(int id)
        {
            return Ok(await _chinookSupervisor.GetGenreById(id));
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<GenreApiModel>> Post([FromBody] GenreApiModel input)
        {
            return Ok(await _chinookSupervisor.AddGenre(input));
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<GenreApiModel>> Put(int id, [FromBody] GenreApiModel input)
        {
            return Ok(await _chinookSupervisor.UpdateGenre(input));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await _chinookSupervisor.DeleteGenre(id));
        }
    }
}