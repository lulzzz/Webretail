using System.Collections.Generic;
using System.Threading.Tasks;
using Webretail.Admin.Models;

namespace Webretail.Admin.Repositories
{
    public interface IProductRepository
    {
		Task<IEnumerable<Product>> GetAsync();

        Task<Product> GetByIdAsync(int id);

        Task InsertAsync(Product model);

        Task UpdateAsync(int id, Product model);

        Task DeleteAsync(int id);

        Task AddCategoryAsync(ProductCategory model);

        Task RemoveCategoryAsync(ProductCategory model);

        Task AddAttributeAsync(ProductAttribute model);

        Task RemoveAttributeAsync(ProductAttribute model);

        Task AddAttributeValueAsync(ProductAttributeValue model);

        Task RemoveAttributeValueAsync(ProductAttributeValue model);

        Task BuildArticlesByIdAsync(int id);
    }
}
