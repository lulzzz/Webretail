using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webretail.Admin.Models;

namespace Webretail.Admin.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        readonly WebretailContext dbContext;

        public AccountRepository(WebretailContext dbContext)
        {
            this.dbContext = dbContext;
        }

		public async Task<IEnumerable<Account>> GetAsync()
        {
			return await dbContext.Account.OrderBy(p => p.AccountId).ToArrayAsync().ConfigureAwait(false);
        }

        public async Task<Account> GetByIdAsync(int id)
        {
			return await dbContext.Account.Where(p => p.AccountId == id).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task InsertAsync(Account model)
        {
            dbContext.Account.Add(model);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateAsync(int id, Account model)
        {
			var item = await dbContext.Account.FirstOrDefaultAsync(p => p.AccountId == id).ConfigureAwait(false);
            if (item != null)
            {
				item.AccountName = model.AccountName;
				item.AccountLastname = model.AccountLastname;
				item.AccountEmail = model.AccountEmail;
				item.AccountPassword = model.AccountPassword;
				item.IsAdmin = model.IsAdmin;
                item.Updated = model.Updated;
                dbContext.Account.Update(item);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task DeleteAsync(int id)
        {
			var item = await dbContext.Account.FirstOrDefaultAsync(p => p.AccountId == id).ConfigureAwait(false);
            if (item != null)
            {
                dbContext.Account.Remove(item);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
        }
    }
}
