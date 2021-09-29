using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chinook.Domain.ApiModels;
using Chinook.Domain.Supervisor;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace Chinook.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    [ApiVersion("1.0")]
    public class EmployeeController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IChinookSupervisor chinookSupervisor, ILogger<EmployeeController> logger)
        {
            _chinookSupervisor = chinookSupervisor;
            _logger = logger;
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        [SwaggerOperation(
            Summary = "Gets all Employee",
            Description = "Gets all Employee",
            OperationId = "Employee.GetAll",
            Tags = new[] { "EmployeeEndpoint" })]
        [Produces("application/json")]
        public async Task<ActionResult<List<EmployeeApiModel>>> Get()
        {
            try
            {
                return new ObjectResult(await _chinookSupervisor.GetAllEmployee());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{id}", Name = "GetEmployeeById")]
        [SwaggerOperation(
            Summary = "Gets a specific Employee",
            Description = "Gets a specific Employee",
            OperationId = "Employee.GetOne",
            Tags = new[] { "EmployeeEndpoint" })]
        [Produces("application/json")]
        public async Task<ActionResult<EmployeeApiModel>> Get(int id)
        {
            try
            {
                var employee = await _chinookSupervisor.GetEmployeeById(id);
                if (employee == null) return NotFound();

                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [MapToApiVersion("1.0")]
        [HttpGet("reportsto/{id}")]
        [SwaggerOperation(
            Summary = "Gets Reports to by Employee",
            Description = "Gets Reports to by Employee",
            OperationId = "Employee.GetReportsTo",
            Tags = new[] { "EmployeeEndpoint" })]
        [Produces("application/json")]
        public async Task<ActionResult<List<EmployeeApiModel>>> GetReportsTo(int id)
        {
            try
            {
                var employee = await _chinookSupervisor.GetEmployeeById(id);
                if (employee == null) return NotFound();

                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [MapToApiVersion("1.0")]
        [HttpGet("directreports/{id}")]
        [SwaggerOperation(
            Summary = "Gets Employee direct reports",
            Description = "Gets Employee direct reports",
            OperationId = "Employee.GetByArtist",
            Tags = new[] { "EmployeeEndpoint" })]
        [Produces("application/json")]
        public async Task<ActionResult<EmployeeApiModel>> GetDirectReports(int id)
        {
            try
            {
                var employee = await _chinookSupervisor.GetEmployeeById(id);

                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [MapToApiVersion("1.0")]
        [HttpPost]
        [SwaggerOperation(
            Summary = "Creates a new Employee",
            Description = "Creates a new Employee",
            OperationId = "Employee.Create",
            Tags = new[] { "EmployeeEndpoint" })]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<EmployeeApiModel>> Post([FromBody] EmployeeApiModel input)
        {
            try
            {
                if (input == null) return BadRequest("Employee is null");
                if (!ModelState.IsValid) return BadRequest("Invalid Employee object");

                var employee = await _chinookSupervisor.AddEmployee(input);

                return CreatedAtRoute("GetEmployeeById", new { id = employee.Id }, employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [MapToApiVersion("1.0")]
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Update an Employee",
            Description = "Update an Employee",
            OperationId = "Employee.Update",
            Tags = new[] { "EmployeeEndpoint" })]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<EmployeeApiModel>> Put(int id, [FromBody] EmployeeApiModel input)
        {
            try
            {
                if (input == null) return BadRequest("Employee is null");
                if (!ModelState.IsValid) return BadRequest("Invalid Employee object");

                if (await _chinookSupervisor.UpdateEmployee(input))
                    return CreatedAtRoute("GetEmployeeById", new { id = input.Id }, input);

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [MapToApiVersion("1.0")]
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete a Employee",
            Description = "Delete a Employee",
            OperationId = "Employee.Delete",
            Tags = new[] { "EmployeeEndpoint" })]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (await _chinookSupervisor.DeleteEmployee(id)) return Ok();

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}