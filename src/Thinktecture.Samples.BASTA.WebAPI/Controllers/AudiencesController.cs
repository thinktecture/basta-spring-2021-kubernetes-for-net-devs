using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Thinktecture.Samples.BASTA.WebAPI.Models;
using Thinktecture.Samples.BASTA.WebAPI.Services;

namespace Thinktecture.Samples.BASTA.WebAPI.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("audiences")]
    public class AudiencesController : ControllerBase
    {
        protected IAudiencesService Service { get; }

        public AudiencesController(IAudiencesService service)
        {
            Service = service;
        }
        
        [HttpGet]
        [Route("")]
        [SwaggerOperation("Get all audiences")]
        [SwaggerResponse(200, "list of audiences", typeof(IEnumerable<AudienceListModel>))]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> GetAllAudiencesAsync()
        {
            var all = await Service.GetAllAsync();
            return Ok(all);
        }
        
        [HttpGet]
        [Route("{id:guid}", Name = "GetAudienceById")]
        [SwaggerOperation("Return a single audience")]
        [SwaggerResponse(200, "the desired audience", typeof(AudienceDetailsModel))]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> GetAudienceByIdAsync([FromRoute]Guid id)
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
        [SwaggerOperation("Create a new audience")]
        [SwaggerResponse(201, "created audience", typeof(AudienceDetailsModel))]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> CreateAudienceAsync([FromBody] AudienceCreateModel model)
        {
            var created = await Service.CreateAsync(model);
            return CreatedAtRoute("GetAudienceById", new {id = created.Id}, created);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [SwaggerOperation("Update an audience")]
        [SwaggerResponse(200, "the updated audience", typeof(AudienceDetailsModel))]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> UpdateAudienceAsync([FromRoute] Guid id, [FromBody] AudienceUpdateModel model)
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
        [SwaggerOperation("Delete a single audience")]
        [SwaggerResponse(204, "Audience has been deleted")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> DeleteAudienceByIdAsync([FromRoute] Guid id)
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
