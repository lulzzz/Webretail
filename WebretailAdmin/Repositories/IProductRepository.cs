using System.Collections.Generic;
using System.Threading.Tasks;
using Webretail.Admin.Models;

namespace Webretail.Admin.Repositories
{
    public interface IProductRepository
    {
		Task<IEnumerable<Product>> GetAsync();

        Task<Product> GetByIdAsync(int id);

        Task InsertAsync(Product value);

        Task UpdateAsync(int id, Product value);

        Task DeleteAsync(int id);
    }
}
