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
    [ApiVersion( "1.0" )]
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
            try  
            {  
                var invoiceLines = await _chinookSupervisor.GetAllInvoiceLine();  

                if (invoiceLines.Any())  
                {  
                    return Ok(invoiceLines);  
                }  
                else  
                {  
                    return StatusCode((int)HttpStatusCode.NotFound, "No InvoiceLines Could Be Found");  
                }  
            }  
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the InvoiceLineController Get action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Get All InvoiceLines");  
            }
        }

        [HttpGet("{id}", Name = "GetInvoiceLineById")]
        [Produces("application/json")]
        public async Task<ActionResult<InvoiceLineApiModel>> Get(int id)
        {
            try  
            {  
                var invoiceLine = await _chinookSupervisor.GetInvoiceLineById(id);  

                if (invoiceLine != null)  
                {  
                    return Ok(invoiceLine);  
                }  
                else  
                {  
                    return StatusCode((int)HttpStatusCode.NotFound, "InvoiceLine Not Found");  
                }  
            }  
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the InvoiceLineController GetById action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Get InvoiceLine By Id");  
            }
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<InvoiceLineApiModel>> Post([FromBody] InvoiceLineApiModel input)
        {
            try  
            {  
                if (input == null)  
                {  
                    return StatusCode((int)HttpStatusCode.BadRequest, "Given InvoiceLine is null");
                }  
                else  
                {
                    return Ok(await _chinookSupervisor.AddInvoiceLine(input));
                }  
            }
            catch (ValidationException  ex)  
            {  
                _logger.LogError($"Something went wrong inside the InvoiceLineController Add InvoiceLine action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Add InvoiceLines");  
            }
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the InvoiceLineController Add InvoiceLine action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Add InvoiceLines");  
            }
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<InvoiceLineApiModel>> Put(int id, [FromBody] InvoiceLineApiModel input)
        {
            try  
            {  
                if (input == null)  
                {  
                    return StatusCode((int)HttpStatusCode.BadRequest, "Given InvoiceLine is null");
                }  
                else  
                {
                    return Ok(await _chinookSupervisor.UpdateInvoiceLine(input));
                }  
            }
            catch (ValidationException  ex)  
            {  
                _logger.LogError($"Something went wrong inside the InvoiceLineController Update InvoiceLine action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Update InvoiceLines");  
            }
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the InvoiceLineController Update InvoiceLine action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Update InvoiceLines");  
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try  
            {  
                return Ok(await _chinookSupervisor.DeleteInvoiceLine(id)); 
            }  
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the InvoiceLineController Delete action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Delete InvoiceLine");  
            }
        }

        [HttpGet("invoice/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<List<InvoiceLineApiModel>>> GetByInvoiceId(int id)
        {
            try  
            {  
                var invoiceLines = await _chinookSupervisor.GetInvoiceLineByInvoiceId(id);  

                if (invoiceLines.Any())  
                {  
                    return Ok(invoiceLines);  
                }  
                else  
                {  
                    return StatusCode((int)HttpStatusCode.NotFound, "No InvoiceLines Could Be Found for the Invoice");  
                }  
            }  
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the InvoiceLineController GetByInvoiceId action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Get All InvoiceLines for Invoice");  
            }
        }

        [HttpGet("track/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<List<InvoiceLineApiModel>>> GetByTrackId(int id)
        {
            try  
            {  
                var invoiceLines = await _chinookSupervisor.GetInvoiceLineByTrackId(id);  

                if (invoiceLines.Any())  
                {  
                    return Ok(invoiceLines);  
                }  
                else  
                {  
                    return StatusCode((int)HttpStatusCode.NotFound, "No InvoiceLines Could Be Found for the Track");  
                }  
            }  
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the InvoiceLineController Get By Track action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Get All InvoiceLines for Track");  
            }
        }
    }
}