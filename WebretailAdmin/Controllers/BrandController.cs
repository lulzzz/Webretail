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
	public class BrandController : Controller
    {
        readonly IBrandRepository repository;

        public BrandController(IBrandRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/brand
        [HttpGet]
        public async Task<IEnumerable<Brand>> Get()
        {
            return await repository.GetAsync().ConfigureAwait(false);
        }

        // GET api/brand/5
        [HttpGet("{id}", Name = "GetByBrandIdRoute")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await repository.GetByIdAsync(id).ConfigureAwait(false);
            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        // POST api/brand
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Brand model)
        {
            if (!ModelState.IsValid)
            {
                return new StatusCodeResult(400);
            }

            await repository.InsertAsync(model).ConfigureAwait(false);

            return CreatedAtRoute(
                "GetByBrandIdRoute",
				new { controller = "Brand", id = model.BrandId },
                model
            );
        }

        // PUT api/brand/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Brand model)
        {
            if (!ModelState.IsValid)
            {
                return new StatusCodeResult(400);
            }

            await repository.UpdateAsync(id, model).ConfigureAwait(false);

            return new NoContentResult();
        }

        // DELETE api/brand/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await repository.DeleteAsync(id).ConfigureAwait(false);

            return new StatusCodeResult(204);
        }
    }
}
