using System.Collections.Generic;
using System.Threading.Tasks;
using Webretail.Admin.Models;

namespace Webretail.Admin.Repositories
{
    public interface IAccountRepository
    {
		Task<IEnumerable<Account>> GetAsync();

        Task<Account> GetByIdAsync(int id);

        Task InsertAsync(Account value);

        Task UpdateAsync(int id, Account value);

        Task DeleteAsync(int id);
    }
}
