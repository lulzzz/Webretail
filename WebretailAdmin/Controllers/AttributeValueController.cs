using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webretail.Admin.Models;
using Webretail.Admin.Repositories;

namespace Webretail.Admin.Controllers
{
    [Route("api/[controller]")]
    public class AttributeValueController : Controller
    {
        readonly IAttributeRepository repository;

        public AttributeValueController(IAttributeRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/attributevalue
        [HttpGet]
        public async Task<IEnumerable<AttributeValue>> Get()
        {
            return await repository.GetValueAsync().ConfigureAwait(false);
        }

        // GET api/attributevalue/5
        [HttpGet("{id}", Name = "GetByAttributeValueIdRoute")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await repository.GetValueByIdAsync(id).ConfigureAwait(false);
            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        // POST api/attributevalue
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AttributeValue model)
        {
            if (!ModelState.IsValid)
            {
                return new StatusCodeResult(400);
            }

            await repository.InsertValueAsync(model).ConfigureAwait(false);

            return CreatedAtRoute(
                "GetByAttributeValueIdRoute",
				new { controller = "AttributeValue", id = model.AttributeValueId },
                model
            );
        }

        // PUT api/attributevalue/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]AttributeValue model)
        {
            if (!ModelState.IsValid)
            {
                return new StatusCodeResult(400);
            }

            await repository.UpdateValueAsync(id, model).ConfigureAwait(false);

            return new NoContentResult();
        }

        // DELETE api/attributevalue/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await repository.DeleteValueAsync(id).ConfigureAwait(false);

            return new StatusCodeResult(204);
        }
    }
}
