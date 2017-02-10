using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webretail.Admin.Models;

namespace Webretail.Admin.Repositories
{
    public class ProductRepository : IProductRepository
    {
        readonly WebretailContext dbContext;

        public ProductRepository(WebretailContext dbContext)
        {
            this.dbContext = dbContext;
        }

		public async Task<IEnumerable<Product>> GetAsync()
        {
            // TODO: optimize query
            var items = await (
                        from p in dbContext.Product 
                        join b in dbContext.Brand on p.BrandId equals b.BrandId
                        join cj in (from pc in dbContext.ProductCategory
                                   join c in dbContext.Category on pc.CategoryId equals c.CategoryId
                                   select new {
                                        ProductId = pc.ProductId,
                                        Category = c
                                   }) on p.ProductId equals cj.ProductId into categories
                        join aj in (from pa in dbContext.ProductAttribute 
                                   join a in dbContext.Attribute on pa.AttributeId equals a.AttributeId 
                                   select new {
                                        ProductId = pa.ProductId,
                                        Attribute = a
                                   }) on p.ProductId equals aj.ProductId into attributes
                        join vj in (from pv in dbContext.ProductAttributeValue 
                                   join a in dbContext.AttributeValue on pv.AttributeValueId equals a.AttributeValueId 
                                   select new {
                                        ProductId = pv.ProductId,
                                        AttributeValue = a
                                   }) on p.ProductId equals vj.ProductId into attributeValues
                        select new Product {
                            ProductId = p.ProductId, 
                            ProductCode = p.ProductCode, 
                            ProductName = p.ProductName, 
                            ProductUm = p.ProductUm, 
                            ProductPrice = p.ProductPrice,
                            BrandId = p.BrandId,
                            Brand = b, 
                            Categories = categories.Select(x => new ProductCategory { Category = x.Category }).ToArray(), 
                            Attributes = attributes.Select(x => new ProductAttribute { Attribute = x.Attribute }).ToArray(),
                            AttributeValues = attributeValues.Select(x => new ProductAttributeValue { AttributeValue = x.AttributeValue }).ToArray()
                        })
                        .ToArrayAsync().ConfigureAwait(false);
            return items;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
           var item = await (
                        from p in dbContext.Product 
                        join b in dbContext.Brand on p.BrandId equals b.BrandId
                        join cj in (from pc in dbContext.ProductCategory
                                   join c in dbContext.Category on pc.CategoryId equals c.CategoryId
                                   select new {
                                        ProductId = pc.ProductId,
                                        Category = c
                                   }) on p.ProductId equals cj.ProductId into categories
                        join aj in (from pa in dbContext.ProductAttribute 
                                   join a in dbContext.Attribute on pa.AttributeId equals a.AttributeId 
                                   select new {
                                        ProductId = pa.ProductId,
                                        Attribute = a
                                   }) on p.ProductId equals aj.ProductId into attributes
                        join vj in (from pv in dbContext.ProductAttributeValue 
                                   join a in dbContext.AttributeValue on pv.AttributeValueId equals a.AttributeValueId 
                                   select new {
                                        ProductId = pv.ProductId,
                                        AttributeValue = a
                                   }) on p.ProductId equals vj.ProductId into attributeValues
                        where p.ProductId == id
                        select new Product {
                            ProductId = p.ProductId, 
                            ProductCode = p.ProductCode, 
                            ProductName = p.ProductName, 
                            ProductUm = p.ProductUm, 
                            ProductPrice = p.ProductPrice,
                            BrandId = p.BrandId,
                            Brand = b, 
                            Updated = p.Updated,
                            Categories = categories.Select(x => new ProductCategory { Category = x.Category }).ToArray(), 
                            Attributes = attributes.Select(x => new ProductAttribute { Attribute = x.Attribute }).ToArray(),
                            AttributeValues = attributeValues.Select(x => new ProductAttributeValue { AttributeValue = x.AttributeValue }).ToArray()
                        })
                        .FirstOrDefaultAsync().ConfigureAwait(false);

            item.Articles = await (
                        from a in dbContext.Article
                        join b in dbContext.ArticleAttributeValue on a.ArticleId equals b.ArticleId
                        join c in dbContext.AttributeValue on b.AttributeValueId equals c.AttributeValueId
                        where a.ProductId == id
                        select new {
                            Article = a,
                            ArticleAttribute = b,
                            AttributeValue = c
                        } into all
                        group all by all.Article.ArticleId into g
                        select new Article {
                            ArticleId = g.Key,
                            //Barcode = g.FirstOrDefault().Article.Barcode,
                            AttributeValues = g.Select(p => new ArticleAttributeValue { AttributeValue = p.AttributeValue }).ToArray()
                        }).ToArrayAsync().ConfigureAwait(false);

			return item;
        }

        public async Task InsertAsync(Product model)
        {
            await dbContext.Product.AddAsync(model).ConfigureAwait(false);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateAsync(int id, Product model)
        {
			var item = await dbContext.Product.FirstOrDefaultAsync(p => p.ProductId == id).ConfigureAwait(false);
            if (item != null)
            {
				item.ProductCode = model.ProductCode;
				item.ProductName = model.ProductName;
				item.BrandId = model.Brand.BrandId;
                item.ProductUm = model.ProductUm;
                item.ProductPrice = model.ProductPrice;
                item.Updated = model.Updated;
                dbContext.Product.Update(item);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task DeleteAsync(int id)
        {
			var item = await dbContext.Product.FirstOrDefaultAsync(p => p.ProductId == id).ConfigureAwait(false);
            if (item != null)
            {
                dbContext.Product.Remove(item);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
        }
    }
}
