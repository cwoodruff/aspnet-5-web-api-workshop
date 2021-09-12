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
    public class InvoiceLineController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;
        private readonly ILogger<InvoiceLineController> _logger;

        public InvoiceLineController(IChinookSupervisor chinookSupervisor, ILogger<InvoiceLineController> logger)
        {
            _chinookSupervisor = chinookSupervisor;
            _logger = logger;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<List<InvoiceLineApiModel>>> Get()
        {
            return Ok(await _chinookSupervisor.GetAllInvoiceLine());
        }

        [HttpGet("{id}", Name = "GetInvoiceLineById")]
        [Produces("application/json")]
        public async Task<ActionResult<InvoiceLineApiModel>> Get(int id)
        {
            return Ok(await _chinookSupervisor.GetInvoiceLineById(id));
        }

        [HttpGet("invoice/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<List<InvoiceLineApiModel>>> GetByInvoiceId(int id)
        {
            return Ok(await _chinookSupervisor.GetInvoiceLineByInvoiceId(id));
        }

        [HttpGet("track/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<List<InvoiceLineApiModel>>> GetByTrackId(int id)
        {
            return Ok(await _chinookSupervisor.GetInvoiceLineByTrackId(id));
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<InvoiceLineApiModel>> Post([FromBody] InvoiceLineApiModel input)
        {
            return Ok(await _chinookSupervisor.AddInvoiceLine(input));
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<InvoiceLineApiModel>> Put(int id, [FromBody] InvoiceLineApiModel input)
        {
            return Ok(await _chinookSupervisor.UpdateInvoiceLine(input));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await _chinookSupervisor.DeleteInvoiceLine(id));
        }
    }
}