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
    public class MediaTypeController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;
        private readonly ILogger<MediaTypeController> _logger;

        public MediaTypeController(IChinookSupervisor chinookSupervisor, ILogger<MediaTypeController> logger)
        {
            _chinookSupervisor = chinookSupervisor;
            _logger = logger;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<List<MediaTypeApiModel>>> Get()
        {
            return Ok(await _chinookSupervisor.GetAllMediaType());
        }

        [HttpGet("{id}", Name = "GetMediaTypeById")]
        [Produces("application/json")]
        public async Task<ActionResult<MediaTypeApiModel>> Get(int id)
        {
            return Ok(await _chinookSupervisor.GetMediaTypeById(id));
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<MediaTypeApiModel>> Post([FromBody] MediaTypeApiModel input)
        {
            return Ok(await _chinookSupervisor.AddMediaType(input));
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<MediaTypeApiModel>> Put(int id, [FromBody] MediaTypeApiModel input)
        {
            return Ok(await _chinookSupervisor.UpdateMediaType(input));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await _chinookSupervisor.DeleteMediaType(id));
        }
    }
}