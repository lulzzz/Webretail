using System.Collections.Generic;
using System.Threading.Tasks;
using Webretail.Admin.Models;

namespace Webretail.Admin.Repositories
{
    public interface IAttributeRepository
    {
		Task<IEnumerable<Attribute>> GetAsync();

        Task<Attribute> GetByIdAsync(int id);

        Task InsertAsync(Attribute value);

        Task UpdateAsync(int id, Attribute value);

        Task DeleteAsync(int id);

		Task<IEnumerable<AttributeValue>> GetValueAsync();

        Task<IEnumerable<AttributeValue>> GetValueByAttributeIdAsync(int id);

        Task<AttributeValue> GetValueByIdAsync(int id);

        Task InsertValueAsync(AttributeValue value);

        Task UpdateValueAsync(int id, AttributeValue value);

        Task DeleteValueAsync(int id);
    }
}
