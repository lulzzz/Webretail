using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webretail.Admin.Models;
using Webretail.Admin.Repositories;

namespace Webretail.Admin.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        readonly ICategoryRepository repository;

        public CategoryController(ICategoryRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/category
        [HttpGet]
        public async Task<IEnumerable<Category>> Get()
        {
            return await repository.GetAsync().ConfigureAwait(false);
        }

        // GET api/category/5
        [HttpGet("{id}", Name = "GetByCategoryIdRoute")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await repository.GetByIdAsync(id).ConfigureAwait(false);
            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        // POST api/category
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Category model)
        {
            if (!ModelState.IsValid)
            {
                return new StatusCodeResult(400);
            }

            await repository.InsertAsync(model).ConfigureAwait(false);

            return CreatedAtRoute(
                "GetByCategoryIdRoute",
				new { controller = "Category", id = model.CategoryId },
                model
            );
        }

        // PUT api/category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Category model)
        {
            if (!ModelState.IsValid)
            {
                return new StatusCodeResult(400);
            }

            await repository.UpdateAsync(id, model).ConfigureAwait(false);

            return new NoContentResult();
        }

        // DELETE api/category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await repository.DeleteAsync(id).ConfigureAwait(false);

            return new StatusCodeResult(204);
        }
    }
}
