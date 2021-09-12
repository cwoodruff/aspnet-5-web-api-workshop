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
    public class EmployeeController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IChinookSupervisor chinookSupervisor, ILogger<EmployeeController> logger)
        {
            _chinookSupervisor = chinookSupervisor;
            _logger = logger;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<List<EmployeeApiModel>>> Get()
        {
            return Ok(await _chinookSupervisor.GetAllEmployee());
        }

        [HttpGet("{id}", Name = "GetEmployeeById")]
        [Produces("application/json")]
        public async Task<ActionResult<EmployeeApiModel>> Get(int id)
        {
            return Ok(await _chinookSupervisor.GetEmployeeById(id));
        }

        [HttpGet("reportsto/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<EmployeeApiModel>> GetReportsTo(int id)
        {
            return Ok(await _chinookSupervisor.GetEmployeeById(id));
        }

        [HttpGet("directreports/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<List<EmployeeApiModel>>> GetDirectReports(int id)
        {
            return Ok(await _chinookSupervisor.GetEmployeeById(id));
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<EmployeeApiModel>> Post([FromBody] EmployeeApiModel input)
        {
            return Ok(await _chinookSupervisor.AddEmployee(input));
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<EmployeeApiModel>> Put(int id, [FromBody] EmployeeApiModel input)
        {
            return Ok(await _chinookSupervisor.UpdateEmployee(input));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await _chinookSupervisor.DeleteEmployee(id));
        }
    }
}