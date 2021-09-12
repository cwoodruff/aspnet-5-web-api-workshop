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
    public class AlbumController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;
        private readonly ILogger<AlbumController> _logger;

        public AlbumController(IChinookSupervisor chinookSupervisor, ILogger<AlbumController> logger)
        {
            _chinookSupervisor = chinookSupervisor;
            _logger = logger;
        }

        [HttpGet]
        [Produces(typeof(List<AlbumApiModel>))]
        public async Task<ActionResult<List<AlbumApiModel>>> Get()
        {
            try  
            {  
                var albums = await _chinookSupervisor.GetAllAlbum();  

                if (albums.Any())  
                {  
                    return Ok(albums);  
                }  
                else  
                {  
                    return StatusCode((int)HttpStatusCode.NotFound, "No Albums Could Be Found");  
                }  
            }  
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Get All Albums");  
            }  
        }

        [HttpGet("{id}", Name = "GetAlbumById")]
        public async Task<ActionResult<AlbumApiModel>> Get(int id)
        {
            try  
            {  
                var album = await _chinookSupervisor.GetAlbumById(id);  

                if (album != null)  
                {  
                    return Ok(album);  
                }  
                else  
                {  
                    return StatusCode((int)HttpStatusCode.NotFound, "Album Not Found");  
                }  
            }  
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the AlbumController GetById action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Get Album By Id");  
            }
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<AlbumApiModel>> Post([FromBody] AlbumApiModel input)
        {
            try  
            {  
                if (input == null)  
                {  
                    return StatusCode((int)HttpStatusCode.BadRequest, "Given Album is null");
                }  
                else  
                {
                    return Ok(await _chinookSupervisor.AddAlbum(input));
                }  
            }
            catch (ValidationException  ex)  
            {  
                _logger.LogError($"Something went wrong inside the AlbumController Add Album action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Add Albums");  
            }
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the AlbumController Add Album action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Add Albums");  
            }
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<AlbumApiModel>> Put(int id, [FromBody] AlbumApiModel input)
        {
            try  
            {  
                if (input == null)  
                {  
                    return StatusCode((int)HttpStatusCode.BadRequest, "Given Album is null");
                }  
                else  
                {
                    return Ok(await _chinookSupervisor.UpdateAlbum(input));
                }  
            }
            catch (ValidationException  ex)  
            {  
                _logger.LogError($"Something went wrong inside the AlbumController Update Album action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Update Albums");  
            }
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the AlbumController Update Album action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Update Albums");  
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try  
            {  
                return Ok(await _chinookSupervisor.DeleteAlbum(id)); 
            }  
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the AlbumController Delete action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Delete Album");  
            }
        }

        [HttpGet("artist/{id}")]
        public async Task<ActionResult<List<AlbumApiModel>>> GetByArtistId(int id)
        {
            try  
            {  
                var albums = await _chinookSupervisor.GetAlbumByArtistId(id);  

                if (albums.Any())  
                {  
                    return Ok(albums);  
                }  
                else  
                {  
                    return StatusCode((int)HttpStatusCode.NotFound, "No Albums Could Be Found for the Artist");  
                }  
            }  
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the AlbumController Get By Artist action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Get All Albums for Artist");  
            }  
        }
    }
}