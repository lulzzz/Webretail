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

        // POST api/product/category
        [HttpPost("category")]
        public async Task<IActionResult> AddCategory([FromBody]ProductCategory model)
        {
            if (!ModelState.IsValid)
            {
                return new StatusCodeResult(400);
            }

            await repository.AddCategoryAsync(model).ConfigureAwait(false);

            return Created("category", model);
        }

        // PUT api/product/category
        [HttpPut("category")]
        public async Task<IActionResult> RemoveCategory([FromBody]ProductCategory model)
        {
            await repository.RemoveCategoryAsync(model).ConfigureAwait(false);

            return new StatusCodeResult(204);
        }

        // POST api/product/attribute
        [HttpPost("attribute")]
        public async Task<IActionResult> AddAttribute([FromBody]ProductAttribute model)
        {
            if (!ModelState.IsValid)
            {
                return new StatusCodeResult(400);
            }

            await repository.AddAttributeAsync(model).ConfigureAwait(false);

            return Created("attribute", model);
        }

        // PUT api/product/attribute
        [HttpPut("attribute")]
        public async Task<IActionResult> RemoveAttribute([FromBody]ProductAttribute model)
        {
            await repository.RemoveAttributeAsync(model).ConfigureAwait(false);

            return new StatusCodeResult(204);
        }

        // POST api/product/attributevalue
        [HttpPost("attributevalue")]
        public async Task<IActionResult> AddAttributeValue([FromBody]ProductAttributeValue model)
        {
            if (!ModelState.IsValid)
            {
                return new StatusCodeResult(400);
            }

            await repository.AddAttributeValueAsync(model).ConfigureAwait(false);

            return Created("attributevalue", model);
        }

        // PUT api/product/attributevalue
        [HttpPut("attributevalue")]
        public async Task<IActionResult> RemoveAttributeValue([FromBody]ProductAttributeValue model)
        {
            await repository.RemoveAttributeValueAsync(model).ConfigureAwait(false);

            return new StatusCodeResult(204);
        }

        // GET api/product/5/build
        [HttpGet("{id}/build")]
        public async Task<IActionResult> BuildArticlesById(int id)
        {
            await repository.BuildArticlesByIdAsync(id).ConfigureAwait(false);

            return Ok();
        }
    }
}
