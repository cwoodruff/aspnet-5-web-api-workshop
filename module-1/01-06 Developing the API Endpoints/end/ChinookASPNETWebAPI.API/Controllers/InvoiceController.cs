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
    public class InvoiceController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;
        private readonly ILogger<InvoiceController> _logger;

        public InvoiceController(IChinookSupervisor chinookSupervisor, ILogger<InvoiceController> logger)
        {
            _chinookSupervisor = chinookSupervisor;
            _logger = logger;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<List<InvoiceApiModel>>> Get()
        {
            return Ok(await _chinookSupervisor.GetAllInvoice());
        }

        [HttpGet("{id}", Name = "GetInvoiceById")]
        [Produces("application/json")]
        public async Task<ActionResult<InvoiceApiModel>> Get(int id)
        {
            return Ok(await _chinookSupervisor.GetInvoiceById(id));
        }

        [HttpGet("customer/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<List<InvoiceApiModel>>> GetByCustomerId(int id)
        {
            return Ok(await _chinookSupervisor.GetInvoiceByCustomerId(id));
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<InvoiceApiModel>> Post([FromBody] InvoiceApiModel input)
        {
            return Ok(await _chinookSupervisor.AddInvoice(input));
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<InvoiceApiModel>> Put(int id, [FromBody] InvoiceApiModel input)
        {
            return Ok(await _chinookSupervisor.UpdateInvoice(input));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await _chinookSupervisor.DeleteInvoice(id));
        }

        [HttpGet("employee/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<List<InvoiceApiModel>>> GetByEmployeeId(int id)
        {
            return Ok(await _chinookSupervisor.GetInvoiceByEmployeeId(id));
        }
    }
}