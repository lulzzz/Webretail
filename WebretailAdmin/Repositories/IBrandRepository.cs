using System.Collections.Generic;
using System.Threading.Tasks;
using Webretail.Admin.Models;

namespace Webretail.Admin.Repositories
{
    public interface IBrandRepository
    {
		Task<IEnumerable<Brand>> GetAsync();

        Task<Brand> GetByIdAsync(int id);

        Task InsertAsync(Brand value);

        Task UpdateAsync(int id, Brand value);

        Task DeleteAsync(int id);
    }
}
