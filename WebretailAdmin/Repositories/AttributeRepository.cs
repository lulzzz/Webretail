using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webretail.Admin.Models;

namespace Webretail.Admin.Repositories
{
    public class AttributeRepository : IAttributeRepository
    {
        readonly WebretailContext dbContext;

        public AttributeRepository(WebretailContext dbContext)
        {
            this.dbContext = dbContext;
        }

		public async Task<IEnumerable<Attribute>> GetAsync()
        {
			return await dbContext.Attribute.ToArrayAsync().ConfigureAwait(false);
        }

        public async Task<Attribute> GetByIdAsync(int id)
        {
			return await dbContext.Attribute
                .Where(p => p.AttributeId == id)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public async Task InsertAsync(Attribute model)
        {
            await dbContext.Attribute.AddAsync(model);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateAsync(int id, Attribute model)
        {
			var item = await dbContext.Attribute
                .FirstOrDefaultAsync(p => p.AttributeId == id)
                .ConfigureAwait(false);
            
            if (item != null)
            {
				item.AttributeName = model.AttributeName;
                item.Updated = model.Updated;
                dbContext.Attribute.Update(item);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task DeleteAsync(int id)
        {
			var item = await dbContext.Attribute
                .FirstOrDefaultAsync(p => p.AttributeId == id)
                .ConfigureAwait(false);

            if (item != null)
            {
                dbContext.Attribute.Remove(item);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
        }

		public async Task<IEnumerable<AttributeValue>> GetValueAsync()
        {
			return await dbContext.AttributeValue.ToArrayAsync().ConfigureAwait(false);
        }

        public async Task<AttributeValue> GetValueByIdAsync(int id)
        {
			return await dbContext.AttributeValue
                .Where(p => p.AttributeValueId == id)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<AttributeValue>> GetValueByAttributeIdAsync(int id)
        {
			return await dbContext.AttributeValue
                .Where(p => p.AttributeId == id)
                .ToArrayAsync()
                .ConfigureAwait(false);
        }

        public async Task InsertValueAsync(AttributeValue model)
        {
            await dbContext.AttributeValue.AddAsync(model);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateValueAsync(int id, AttributeValue model)
        {
			var item = await dbContext.AttributeValue.FirstOrDefaultAsync(p => p.AttributeValueId == id).ConfigureAwait(false);
            if (item != null)
            {
				item.AttributeValueName = model.AttributeValueName;
                item.Updated = model.Updated;
                dbContext.AttributeValue.Update(item);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task DeleteValueAsync(int id)
        {
			var item = await dbContext.AttributeValue.FirstOrDefaultAsync(p => p.AttributeValueId == id).ConfigureAwait(false);
            if (item != null)
            {
                dbContext.AttributeValue.Remove(item);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
        }
    }
}
