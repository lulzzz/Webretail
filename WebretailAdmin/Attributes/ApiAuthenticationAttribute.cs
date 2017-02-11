using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using Webretail.Admin.Models;

namespace Webretail.Admin.Attributes
{
    public class ApiAuthenticationAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        IMemoryCache memoryCache;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            StringValues token;
            if (context.HttpContext.Request.Headers.TryGetValue("token", out token))
            {
                if (memoryCache == null)
                    memoryCache = (IMemoryCache)context.HttpContext.RequestServices.GetService(typeof(IMemoryCache));

                TokenModel tokenModel;
                if (memoryCache.TryGetValue(token.ToString(), out tokenModel))
                {
                    if (Roles.Equals("Admin"))
                    {
                        if (tokenModel.Role.Equals(Roles))
                            GrantAccess(context, token, Roles);
                    }
                    else
                        GrantAccess(context, token, Roles);
                    return;
                }
            }

            context.Result = new UnauthorizedResult();
        }

        void GrantAccess(AuthorizationFilterContext context, string token, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Hash, token),
                new Claim(ClaimTypes.Role, role)
            };
            var id = new ClaimsIdentity(claims, "Basic");
            var principal = new ClaimsPrincipal(new[] {id});
            context.HttpContext.User = principal;
        }
    }
}