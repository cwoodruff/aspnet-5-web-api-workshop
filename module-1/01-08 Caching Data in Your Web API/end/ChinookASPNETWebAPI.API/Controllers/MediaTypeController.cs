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
    [ResponseCache(Duration = 604800)]
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
            try  
            {  
                var mediaTypes = await _chinookSupervisor.GetAllMediaType();  

                if (mediaTypes.Any())  
                {  
                    return Ok(mediaTypes);  
                }  
                else  
                {  
                    return StatusCode((int)HttpStatusCode.NotFound, "No MediaType Could Be Found");  
                }  
            }  
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the MediaTypeController Get action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Get All MediaType");  
            }
        }

        [HttpGet("{id}", Name = "GetMediaTypeById")]
        [Produces("application/json")]
        public async Task<ActionResult<MediaTypeApiModel>> Get(int id)
        {
            try  
            {  
                var mediaType = await _chinookSupervisor.GetMediaTypeById(id);  

                if (mediaType != null)  
                {  
                    return Ok(mediaType);  
                }  
                else  
                {  
                    return StatusCode((int)HttpStatusCode.NotFound, "MediaType Not Found");  
                }  
            }  
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the MediaTypeController GetById action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Get MediaType By Id");  
            }
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<MediaTypeApiModel>> Post([FromBody] MediaTypeApiModel input)
        {
            try  
            {  
                if (input == null)  
                {  
                    return StatusCode((int)HttpStatusCode.BadRequest, "Given MediaType is null");
                }  
                else  
                {
                    return Ok(await _chinookSupervisor.AddMediaType(input));
                }  
            }
            catch (ValidationException  ex)  
            {  
                _logger.LogError($"Something went wrong inside the MediaTypeController Add MediaType action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Add MediaType");  
            }
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the MediaTypeController Add MediaType action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Add MediaType");  
            }
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<MediaTypeApiModel>> Put(int id, [FromBody] MediaTypeApiModel input)
        {
            try  
            {  
                if (input == null)  
                {  
                    return StatusCode((int)HttpStatusCode.BadRequest, "Given MediaType is null");
                }  
                else  
                {
                    return Ok(await _chinookSupervisor.UpdateMediaType(input));
                }  
            }
            catch (ValidationException  ex)  
            {  
                _logger.LogError($"Something went wrong inside the MediaTypeController Update MediaType action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Update MediaType");  
            }
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the MediaTypeController Update MediaType action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Update MediaType");  
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try  
            {  
                return Ok(await _chinookSupervisor.DeleteMediaType(id)); 
            }  
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the MediaTypeController Delete action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Delete MediaType");  
            }
        }
    }
}