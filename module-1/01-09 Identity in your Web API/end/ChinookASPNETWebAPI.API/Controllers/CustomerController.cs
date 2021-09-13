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
            try  
            {  
                var customers = await _chinookSupervisor.GetAllCustomer();  

                if (customers.Any())  
                {  
                    return Ok(customers);  
                }  
                else  
                {  
                    return StatusCode((int)HttpStatusCode.NotFound, "No Customers Could Be Found");  
                }  
            }  
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the CustomerController Get action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Get All Customers");  
            }
        }

        [HttpGet("{id}", Name = "GetCustomerById")]
        [Produces("application/json")]
        public async Task<ActionResult<CustomerApiModel>> Get(int id)
        {
            try  
            {  
                var customer = await _chinookSupervisor.GetCustomerById(id);  

                if (customer != null)  
                {  
                    return Ok(customer);  
                }  
                else  
                {  
                    return StatusCode((int)HttpStatusCode.NotFound, "Customer Not Found");  
                }  
            }  
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the CustomerController GetById action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Get Customer By Id");  
            }
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<CustomerApiModel>> Post([FromBody] CustomerApiModel input)
        {
            try  
            {  
                if (input == null)  
                {  
                    return StatusCode((int)HttpStatusCode.BadRequest, "Given Customer is null");
                }  
                else  
                {
                    return Ok(await _chinookSupervisor.AddCustomer(input));
                }  
            }
            catch (ValidationException  ex)  
            {  
                _logger.LogError($"Something went wrong inside the CustomerController Add Customer action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Add Customers");  
            }
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the CustomerController Add Customer action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Add Customers");  
            }
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<CustomerApiModel>> Put(int id, [FromBody] CustomerApiModel input)
        {
            try  
            {  
                if (input == null)  
                {  
                    return StatusCode((int)HttpStatusCode.BadRequest, "Given Customer is null");
                }  
                else  
                {
                    return Ok(await _chinookSupervisor.UpdateCustomer(input));
                }  
            }
            catch (ValidationException  ex)  
            {  
                _logger.LogError($"Something went wrong inside the CustomerController Update Customer action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Update Customers");  
            }
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the CustomerController Add Customer action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Update Customers");  
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try  
            {  
                return Ok(await _chinookSupervisor.DeleteCustomer(id)); 
            }  
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the CustomerController Delete action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Delete Customer");  
            }
        }

        [HttpGet("supportrep/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<EmployeeApiModel>> GetBySupportRepId(int id)
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
                    return StatusCode((int)HttpStatusCode.NotFound, "No Support Rep Could Be Found");  
                }  
            }  
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the CustomerController GetBySupportRepId action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing GetBySupportRepId");  
            }
        }
    }
}