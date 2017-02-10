using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webretail.Admin.Models;
using Webretail.Admin.Repositories;

namespace Webretail.Admin.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        readonly IProductRepository repository;

        public ProductController(IProductRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/product
        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            return await repository.GetAsync().ConfigureAwait(false);
        }

        // GET api/product/5
        [HttpGet("{id}", Name = "GetByProductIdRoute")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await repository.GetByIdAsync(id).ConfigureAwait(false);
            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        // POST api/product
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Product model)
        {
            if (!ModelState.IsValid)
            {
                return new StatusCodeResult(400);
            }

            await repository.InsertAsync(model).ConfigureAwait(false);

            return CreatedAtRoute(
                "GetByProductIdRoute",
				new { controller = "Product", id = model.ProductId },
                model
            );
        }

        // PUT api/product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Product model)
        {
            if (!ModelState.IsValid)
            {
                return new StatusCodeResult(400);
            }

            await repository.UpdateAsync(id, model).ConfigureAwait(false);

            return new NoContentResult();
        }

        // DELETE api/product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await repository.DeleteAsync(id).ConfigureAwait(false);

            return new StatusCodeResult(204);
        }
    }
}
