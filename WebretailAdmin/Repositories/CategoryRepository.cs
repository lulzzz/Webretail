using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webretail.Admin.Models;

namespace Webretail.Admin.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        readonly WebretailContext dbContext;

        public CategoryRepository(WebretailContext dbContext)
        {
            this.dbContext = dbContext;
        }

		public async Task<IEnumerable<Category>> GetAsync()
        {
			return await dbContext.Category.OrderBy(p => p.CategoryId).ToArrayAsync().ConfigureAwait(false);
        }

        public async Task<Category> GetByIdAsync(int id)
        {
			return await dbContext.Category.Where(p => p.CategoryId == id).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task InsertAsync(Category model)
        {
            dbContext.Category.Add(model);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateAsync(int id, Category model)
        {
			var item = await dbContext.Category.FirstOrDefaultAsync(p => p.CategoryId == id).ConfigureAwait(false);
            if (item != null)
            {
				item.CategoryName = model.CategoryName;
                item.Updated = model.Updated;
                dbContext.Category.Update(item);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task DeleteAsync(int id)
        {
			var item = await dbContext.Category.FirstOrDefaultAsync(p => p.CategoryId == id).ConfigureAwait(false);
            if (item != null)
            {
                dbContext.Category.Remove(item);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
        }
    }
}
