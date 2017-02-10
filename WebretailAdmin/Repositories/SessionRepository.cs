using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Webretail.Admin.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Webretail.Admin.Helpers;

namespace Webretail.Admin.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        readonly WebretailContext dbContext;
        readonly IMemoryCache memoryCache;

        public SessionRepository(WebretailContext dbContext, IMemoryCache memoryCache)
        {
            this.dbContext = dbContext;
            this.memoryCache = memoryCache;
        }

        public async Task<TokenModel> CreateAsync(LoginModel model)
        {
            var item = await dbContext.Account
                .Where(p => p.AccountEmail == model.Username && p.AccountPassword == model.PasswordCrypted)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            
            if (item == null)
                return null;

            var token = $"{item}{DateTime.Now.Ticks}";
            var tokenModel = new TokenModel
            {
                Token = CryptHelper.SHA1HashStringForUTF8String(token),
                Role = item.IsAdmin ? "Admin" : "User",
                Expiry = DateTime.Now.AddHours(1)
            };

            memoryCache.Set(
                tokenModel.Token,
                tokenModel, 
                new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromHours(1)
                });

            return tokenModel;
        }

        public void Remove(string token)
        {
            memoryCache.Remove(token);
        }
    }
}
