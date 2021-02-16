using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Thinktecture.Samples.BASTA.WebAPI.Models;
using Thinktecture.Samples.BASTA.WebAPI.Repositories;
using Thinktecture.Samples.BASTA.WebAPI.Services;

namespace Thinktecture.Samples.BASTA.WebAPI.Controllers
{
    [ApiController]
    [Route("speakers")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class SpeakersController : ControllerBase
    {
        protected ISpeakersService Service { get; }

        public SpeakersController(ISpeakersService service)
        {
            Service = service;
        }
        
        [HttpGet]
        [Route("")]
        [SwaggerOperation("Get all speakers")]
        [SwaggerResponse(200, "list of speakers", typeof(IEnumerable<SpeakerListModel>))]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> GetAllSpeakersAsync()
        {
            var all = await Service.GetAllAsync();
            return Ok(all);
        }
        
        [HttpGet]
        [Route("{id:guid}", Name = "GetSpeakerById")]
        [SwaggerOperation("Return a single speaker")]
        [SwaggerResponse(200, "the desired speaker", typeof(SpeakerDetailsModel))]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> GetSpeakerByIdAsync([FromRoute]Guid id)
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
        [SwaggerOperation("Create a new speaker")]
        [SwaggerResponse(201, "speaker audience", typeof(SpeakerDetailsModel))]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> CreateSpeakerAsync([FromBody] SpeakerCreateModel model)
        {
            var created = await Service.CreateAsync(model);
            return CreatedAtRoute("GetSpeakerById", new {id = created.Id}, created);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [SwaggerOperation("Update a speaker")]
        [SwaggerResponse(200, "the updated speaker", typeof(SpeakerDetailsModel))]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> UpdateSpeakerAsync([FromRoute] Guid id, [FromBody] SpeakerUpdateModel model)
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
        [SwaggerOperation("Delete a single speaker")]
        [SwaggerResponse(204, "Speaker has been deleted")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> DeleteSpeakerByIdAsync([FromRoute] Guid id)
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
