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
	public class AccountController : Controller
    {
        readonly IAccountRepository repository;

        public AccountController(IAccountRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/user
        [HttpGet]
        public async Task<IEnumerable<Account>> Get()
        {
            return await repository.GetAsync().ConfigureAwait(false);
        }

        // GET api/account/5
        [HttpGet("{id}", Name = "GetByAccountIdRoute")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await repository.GetByIdAsync(id).ConfigureAwait(false);
            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        // POST api/account
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Account model)
        {
            if (!ModelState.IsValid)
            {
                return new StatusCodeResult(400);
            }

            await repository.InsertAsync(model).ConfigureAwait(false);

            return CreatedAtRoute(
                "GetByAccountIdRoute",
				new { controller = "Account", id = model.AccountId },
                model
            );
        }

        // PUT api/account/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Account model)
        {
            if (!ModelState.IsValid)
            {
                return new StatusCodeResult(400);
            }

            await repository.UpdateAsync(id, model).ConfigureAwait(false);

            return new NoContentResult();
        }

        // DELETE api/account/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await repository.DeleteAsync(id).ConfigureAwait(false);

            return new StatusCodeResult(204);
        }
    }
}
