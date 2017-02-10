using System.Collections.Generic;
using System.Threading.Tasks;
using Webretail.Admin.Models;

namespace Webretail.Admin.Repositories
{
    public interface ICategoryRepository
    {
		Task<IEnumerable<Category>> GetAsync();

        Task<Category> GetByIdAsync(int id);

        Task InsertAsync(Category value);

        Task UpdateAsync(int id, Category value);

        Task DeleteAsync(int id);
    }
}
