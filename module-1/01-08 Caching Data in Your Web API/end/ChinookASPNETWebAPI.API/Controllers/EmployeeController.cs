using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ChinookASPNETWebAPI.Domain.ApiModels;
using ChinookASPNETWebAPI.Domain.Supervisor;
using FluentValidation;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChinookASPNETWebAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
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
            try  
            {  
                var employees = await _chinookSupervisor.GetAllEmployee();  

                if (employees.Any())  
                {  
                    return Ok(employees);  
                }  
                else  
                {  
                    return StatusCode((int)HttpStatusCode.NotFound, "No Employees Could Be Found");  
                }  
            }  
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the EmployeeController Get action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Get All Employees");  
            }
        }

        [HttpGet("{id}", Name = "GetEmployeeById")]
        [Produces("application/json")]
        public async Task<ActionResult<EmployeeApiModel>> Get(int id)
        {
            try  
            {  
                var employee = await _chinookSupervisor.GetEmployeeById(id);  

                if (employee != null)  
                {  
                    return Ok(employee);  
                }  
                else  
                {  
                    return StatusCode((int)HttpStatusCode.NotFound, "Employee Not Found");  
                }  
            }  
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the EmployeeController GetById action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Get Employee By Id");  
            }
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<EmployeeApiModel>> Post([FromBody] EmployeeApiModel input)
        {
            try  
            {  
                if (input == null)  
                {  
                    return StatusCode((int)HttpStatusCode.BadRequest, "Given Employee is null");
                }  
                else  
                {
                    return Ok(await _chinookSupervisor.AddEmployee(input));
                }  
            }
            catch (ValidationException  ex)  
            {  
                _logger.LogError($"Something went wrong inside the EmployeeController Add Employee action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Add Employee");  
            }
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the EmployeeController Add Employee action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Add Employee");  
            }
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<EmployeeApiModel>> Put(int id, [FromBody] EmployeeApiModel input)
        {
            try  
            {  
                if (input == null)  
                {  
                    return StatusCode((int)HttpStatusCode.BadRequest, "Given Employee is null");
                }  
                else  
                {
                    return Ok(await _chinookSupervisor.UpdateEmployee(input));
                }  
            }
            catch (ValidationException  ex)  
            {  
                _logger.LogError($"Something went wrong inside the EmployeeController Update Employee action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Update Employee");  
            }
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the EmployeeController Update Employee action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Update Employee");  
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try  
            {  
                return Ok(await _chinookSupervisor.DeleteEmployee(id)); 
            }  
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the EmployeeController Delete action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Delete Employee");  
            }
        }

        [HttpGet("reportsto/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<EmployeeApiModel>> GetReportsTo(int id)
        {
            try  
            {  
                var employee = await _chinookSupervisor.GetEmployeeById(id);  

                if (employee != null)  
                {  
                    return Ok(employee);  
                }  
                else  
                {  
                    return StatusCode((int)HttpStatusCode.NotFound, "No Reporting Employees Could Be Found for the Employee");  
                }  
            }  
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the EmployeeController GetReportsTo action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Get GetReportsTo for Employee");  
            }
        }

        [HttpGet("directreports/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<List<EmployeeApiModel>>> GetDirectReports(int id)
        {
            try  
            {  
                var employees = await _chinookSupervisor.GetDirectReports(id);  

                if (employees.Any())  
                {  
                    return Ok(employees);  
                }  
                else  
                {  
                    return StatusCode((int)HttpStatusCode.NotFound, "No Employees Could Be Found");  
                }  
            }  
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the EmployeeController GetDirectReports action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing GetDirectReports for Employee");  
            }
        }
    }
}