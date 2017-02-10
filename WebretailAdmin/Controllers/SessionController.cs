using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webretail.Admin.Models;
using Webretail.Admin.Repositories;

namespace Webretail.Admin.Controllers
{
    [Route("api/[controller]")]
    public class SessionController : Controller
    {
        readonly ISessionRepository repository;

        public SessionController(ISessionRepository repository)
        {
            this.repository = repository;
        }

        // POST: api/session/login
        [HttpPost("login")]
        public async Task<IActionResult> CreateAsync([FromBody] LoginModel model)
        {
			if (!ModelState.IsValid)
			{
				return new StatusCodeResult(400);
			}

			var item = await repository.CreateAsync(model).ConfigureAwait(false);
			if (item == null)
			{
				return new UnauthorizedResult();
			}

			return CreatedAtRoute(
				"CloseSessionRoute",
				new { controller = "Session", id = item.Token },
				item
			);
		}

		// POST: api/session/logout
		[HttpPost("logout", Name = "CloseSessionRoute")]
		public IActionResult Close([FromBody] TokenModel model)
		{
			if (model == null || !ModelState.IsValid)
			{
				return new StatusCodeResult(400);
			}

			repository.Remove(model.Token);

			return new NoContentResult();
		}
	}
}
