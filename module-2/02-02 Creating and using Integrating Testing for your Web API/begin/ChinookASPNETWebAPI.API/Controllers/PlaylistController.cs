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
    public class PlaylistController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;
        private readonly ILogger<PlaylistController> _logger;

        public PlaylistController(IChinookSupervisor chinookSupervisor, ILogger<PlaylistController> logger)
        {
            _chinookSupervisor = chinookSupervisor;
            _logger = logger;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<List<PlaylistApiModel>>> Get()
        {
            try  
            {  
                var playlists = await _chinookSupervisor.GetAllPlaylist();  

                if (playlists.Any())  
                {  
                    return Ok(playlists);  
                }  
                else  
                {  
                    return StatusCode((int)HttpStatusCode.NotFound, "No Playlists Could Be Found");  
                }  
            }  
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the PlaylistController Get action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Get All Playlists");  
            }
        }

        [HttpGet("{id}", Name = "GetPlaylistById")]
        [Produces("application/json")]
        public async Task<ActionResult<PlaylistApiModel>> Get(int id)
        {
            try  
            {  
                var playlist = await _chinookSupervisor.GetPlaylistById(id);  

                if (playlist != null)  
                {  
                    return Ok(playlist);  
                }  
                else  
                {  
                    return StatusCode((int)HttpStatusCode.NotFound, "Playlist Not Found");  
                }  
            }  
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the PlaylistController GetById action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Get Playlist By Id");  
            }
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<PlaylistApiModel>> Post([FromBody] PlaylistApiModel input)
        {
            try  
            {  
                if (input == null)  
                {  
                    return StatusCode((int)HttpStatusCode.BadRequest, "Given Playlist is null");
                }  
                else  
                {
                    return Ok(await _chinookSupervisor.AddPlaylist(input));
                }  
            }
            catch (ValidationException  ex)  
            {  
                _logger.LogError($"Something went wrong inside the PlaylistController Add Playlist action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Add Playlists");  
            }
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the PlaylistController Add Playlist action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Add Playlists");  
            }
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<PlaylistApiModel>> Put(int id, [FromBody] PlaylistApiModel input)
        {
            try  
            {  
                if (input == null)  
                {  
                    return StatusCode((int)HttpStatusCode.BadRequest, "Given Playlist is null");
                }  
                else  
                {
                    return Ok(await _chinookSupervisor.UpdatePlaylist(input));
                }  
            }
            catch (ValidationException  ex)  
            {  
                _logger.LogError($"Something went wrong inside the PlaylistController Add Playlist action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Add Playlists");  
            }
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the PlaylistController Add Playlist action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Add Playlists");  
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try  
            {  
                return Ok(await _chinookSupervisor.DeletePlaylist(id)); 
            }  
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the PlaylistController GetById action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Get Playlist By Id");  
            }
        }

        [HttpGet("track/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<List<PlaylistApiModel>>> GetByTrackId(int id)
        {
            try  
            {  
                var playlists = await _chinookSupervisor.GetPlaylistByTrackId(id);  

                if (playlists.Any())  
                {  
                    return Ok(playlists);  
                }  
                else  
                {  
                    return StatusCode((int)HttpStatusCode.NotFound, "No Playlists Could Be Found for the Track");  
                }  
            }  
            catch (Exception ex)  
            {  
                _logger.LogError($"Something went wrong inside the PlaylistController GetByTrackId action: {ex}");  
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while executing Get All Playlists for Track");  
            }
        }
    }
}