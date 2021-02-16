using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Thinktecture.Samples.BASTA.Entities;
using Thinktecture.Samples.BASTA.WebAPI.Models;
using Thinktecture.Samples.BASTA.WebAPI.Services;

namespace Thinktecture.Samples.BASTA.WebAPI.Controllers
{
    [ApiController]
    [Route("sessions")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class SessionsController : ControllerBase
    {
        public ISessionsService Service { get; }

        public SessionsController(ISessionsService service)
        {
            Service = service;
        }
        
        [HttpGet]
        [Route("")]
        [SwaggerOperation("Get all sessions")]
        [SwaggerResponse(200, "list of sessions", typeof(IEnumerable<SessionListModel>))]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> GetAllSessionsAsync()
        {
            var all = await Service.GetAllAsync();
            return Ok(all);
        }

        [HttpGet]
        [Route("byspeaker/{speakerId:guid}")]
        [SwaggerOperation("Get all sessions from a certain audience")]
        [SwaggerResponse(200, "list of sessions", typeof(IEnumerable<SessionListModel>))]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> GetAllSessionsBySpeakerAsync([FromRoute] Guid speakerId)
        {
            var all = await Service.GetAllBySpeakerAsync(speakerId);
            return Ok(all);
        }

        [HttpGet]
        [Route("byaudience/{audienceId:guid}")]
        [SwaggerOperation("Get all sessions for a certain audience")]
        [SwaggerResponse(200, "list of sessions", typeof(IEnumerable<SessionListModel>))]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> GetAllSessionsByAudienceAsync([FromRoute] Guid audienceId)
        {
            var all = await Service.GetAllByAudienceAsync(audienceId);
            return Ok(all);
        }

        [HttpGet]
        [Route("{id:guid}", Name = "GetSessionById")]
        [SwaggerOperation("Return a single session")]
        [SwaggerResponse(200, "the desired session", typeof(SessionDetailsModel))]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> GetSessionByIdAsync([FromRoute] Guid id)
        {
            var found = await Service.GetByIdAsync(id);
            if (found == null)
            {
                return NotFound();
            }

            return Ok(found);
        }

        [HttpPost]
        [Route("")]
        [SwaggerOperation("Create a new session")]
        [SwaggerResponse(201, "created session", typeof(SessionDetailsModel))]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> CreateSessionAsync([FromBody] SessionCreateModel model)
        {
            try
            {
                var created = await Service.CreateAsync(model);
                return CreatedAtRoute("GetSessionById", new {id = created.Id}, created);
            }
            catch (IndexOutOfRangeException)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        [SwaggerOperation("Update a session")]
        [SwaggerResponse(200, "the updated session", typeof(SessionDetailsModel))]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> UpdateSessionByIdAsync([FromRoute] Guid id,
            [FromBody] SessionUpdateModel model)
        {
            var updated = await Service.UpdateAsync(id, model);
            if (updated == null)
            {
                return NotFound();
            }
            return Ok(updated);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [SwaggerOperation("Delete a single session")]
        [SwaggerResponse(204, "Session has been deleted")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> DeleteSessionByIdAsync([FromRoute] Guid id)
        {
            var deleted = await Service.DeleteByIdAsync(id);
            if (deleted)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
