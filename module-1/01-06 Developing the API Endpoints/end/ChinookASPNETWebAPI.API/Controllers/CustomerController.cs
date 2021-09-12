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
    public class CustomerController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(IChinookSupervisor chinookSupervisor, ILogger<CustomerController> logger)
        {
            _chinookSupervisor = chinookSupervisor;
            _logger = logger;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<List<CustomerApiModel>>> Get()
        {
            return Ok(await _chinookSupervisor.GetAllCustomer());
        }

        [HttpGet("{id}", Name = "GetCustomerById")]
        [Produces("application/json")]
        public async Task<ActionResult<CustomerApiModel>> Get(int id)
        {
            return Ok(await _chinookSupervisor.GetCustomerById(id));
        }

        [HttpGet("supportrep/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<EmployeeApiModel>> GetBySupportRepId(int id)
        {
            return Ok(await _chinookSupervisor.GetEmployeeById(id));
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<CustomerApiModel>> Post([FromBody] CustomerApiModel input)
        {
            return Ok(await _chinookSupervisor.AddCustomer(input));
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<CustomerApiModel>> Put(int id, [FromBody] CustomerApiModel input)
        {
            return Ok(await _chinookSupervisor.UpdateCustomer(input));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await _chinookSupervisor.DeleteCustomer(id));
        }
    }
}