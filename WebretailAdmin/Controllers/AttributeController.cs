using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webretail.Admin.Attributes;
using Webretail.Admin.Models;
using Webretail.Admin.Repositories;

namespace Webretail.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiAuthentication(Roles = "User")]
	public class AttributeController : Controller
    {
        readonly IAttributeRepository repository;

        public AttributeController(IAttributeRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/attribute
        [HttpGet]
        public async Task<IEnumerable<Attribute>> Get()
        {
            return await repository.GetAsync().ConfigureAwait(false);
        }

        // GET: api/attribute/5/value
        [HttpGet("{id}/value")]
        public async Task<IEnumerable<AttributeValue>> GetValueById(int id)
        {
            return await repository.GetValueByAttributeIdAsync(id).ConfigureAwait(false);
        }

        // GET api/attribute/5
        [HttpGet("{id}", Name = "GetByAttributeIdRoute")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await repository.GetByIdAsync(id).ConfigureAwait(false);
            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        // POST api/attribute
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Attribute model)
        {
            if (!ModelState.IsValid)
            {
                return new StatusCodeResult(400);
            }

            await repository.InsertAsync(model).ConfigureAwait(false);

            return CreatedAtRoute(
                "GetByAttributeIdRoute",
				new { controller = "Attribute", id = model.AttributeId },
                model
            );
        }

        // PUT api/attribute/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Attribute model)
        {
            if (!ModelState.IsValid)
            {
                return new StatusCodeResult(400);
            }

            await repository.UpdateAsync(id, model).ConfigureAwait(false);

            return new NoContentResult();
        }

        // DELETE api/attribute/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await repository.DeleteAsync(id).ConfigureAwait(false);

            return new StatusCodeResult(204);
        }
    }
}
