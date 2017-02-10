using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webretail.Admin.Models;

namespace Webretail.Admin.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        readonly WebretailContext dbContext;

        public BrandRepository(WebretailContext dbContext)
        {
            this.dbContext = dbContext;
        }

		public async Task<IEnumerable<Brand>> GetAsync()
        {
			return await dbContext.Brand.OrderBy(p => p.BrandId).ToArrayAsync().ConfigureAwait(false);
        }

        public async Task<Brand> GetByIdAsync(int id)
        {
			return await dbContext.Brand.Where(p => p.BrandId == id).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task InsertAsync(Brand model)
        {
            await dbContext.Brand.AddAsync(model);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateAsync(int id, Brand model)
        {
			var item = await dbContext.Brand.FirstOrDefaultAsync(p => p.BrandId == id).ConfigureAwait(false);
            if (item != null)
            {
				item.BrandName = model.BrandName;
                item.Updated = model.Updated;
                dbContext.Brand.Update(item);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task DeleteAsync(int id)
        {
			var item = await dbContext.Brand.FirstOrDefaultAsync(p => p.BrandId == id).ConfigureAwait(false);
            if (item != null)
            {
                dbContext.Brand.Remove(item);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
        }
    }
}
