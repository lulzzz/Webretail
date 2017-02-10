using Microsoft.EntityFrameworkCore;

namespace Webretail.Admin.Models
{
    public class WebretailContext : DbContext
    {
        public WebretailContext(DbContextOptions options) : base(options)
        {
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Account>()
			            .HasIndex(b => b.AccountEmail)
			            .IsUnique();

            modelBuilder.Entity<Brand>()
		                .HasIndex(k => k.BrandName)
						.IsUnique();

            modelBuilder.Entity<Category>()
		                .HasIndex(k => k.CategoryName)
						.IsUnique();

            modelBuilder.Entity<Attribute>()
		                .HasIndex(k => k.AttributeName)
						.IsUnique();

			modelBuilder.Entity<Product>()
			            .HasIndex(b => b.ProductName)
			            .IsUnique();

			modelBuilder.Entity<Article>()
						.HasIndex(b => b.Barcode)
			            .IsUnique();

            modelBuilder.Entity<ProductCategory>()
		                .HasKey(k => new { k.ProductId, k.CategoryId });

            modelBuilder.Entity<ProductAttribute>()
		                .HasKey(k => new { k.ProductId, k.AttributeId });

            modelBuilder.Entity<ProductAttributeValue>()
		                .HasKey(k => new { k.ProductId, k.AttributeValueId });

            modelBuilder.Entity<ArticleAttributeValue>()
		                .HasKey(k => new { k.ArticleId, k.AttributeId });
						
		}

		public DbSet<Account> Account { get; set; }

		public DbSet<Brand> Brand { get; set; }

		public DbSet<Category> Category { get; set; }

		public DbSet<ProductCategory> ProductCategory { get; set; }

		public DbSet<Attribute> Attribute { get; set; }

		public DbSet<AttributeValue> AttributeValue { get; set; }

		public DbSet<Product> Product { get; set; }

		public DbSet<ProductAttribute> ProductAttribute { get; set; }

		public DbSet<ProductAttributeValue> ProductAttributeValue { get; set; }

		public DbSet<Article> Article { get; set; }

		public DbSet<ArticleAttributeValue> ArticleAttributeValue { get; set; }

		public DbSet<Publish> Publish { get; set; }


        public void CreateTablesIfNotExists()
        {
			//if (Database.EnsureDeleted())
			if (Database.EnsureCreated())
			{
				Account.Add(new Account { 
					AccountName = "Gerardo", 
					AccountLastname = "Grisolini", 
					AccountEmail = "gerardo@grisolini.com",
					AccountPassword = "{SSHA}7ea4912022463b0c1342562015f114768c41c1ec",
					IsAdmin = true
				});
				SaveChanges();

				CreateDataForDemo();
			}
        }

		public void CreateDataForDemo()
        {
			// Attribute
			var attribute0 = new Attribute { AttributeName = "Season" };
			var attribute1 = new Attribute { AttributeName = "Texture" };
			var attribute2 = new Attribute { AttributeName = "Color" };
			var attribute3 = new Attribute { AttributeName = "Size" };
			Attribute.Add(attribute0);
			Attribute.Add(attribute1);
			Attribute.Add(attribute2);
			Attribute.Add(attribute3);
			SaveChanges();

			// AttributeValue
			var attributeValue0 = new AttributeValue {
				AttributeId = attribute0.AttributeId,
				AttributeValueCode = "2017",
				AttributeValueName = "2017"
			};
			var attributeValue1 = new AttributeValue {
				AttributeId = attribute1.AttributeId,
				AttributeValueCode = "001",
				AttributeValueName = "Casentino"
			};
			var attributeValue2 = new AttributeValue {
				AttributeId = attribute2.AttributeId,
				AttributeValueCode = "00015",
				AttributeValueName = "Orange"
			};
			var attributeValue3a = new AttributeValue {
				AttributeId = attribute3.AttributeId,
				AttributeValueCode = "S",
				AttributeValueName = "Small"
			};
			var attributeValue3b = new AttributeValue {
				AttributeId = attribute3.AttributeId,
				AttributeValueCode = "M",
				AttributeValueName = "Medium"
			};
			AttributeValue.Add(attributeValue0);
			AttributeValue.Add(attributeValue1);
			AttributeValue.Add(attributeValue2);
			AttributeValue.Add(attributeValue3a);
			AttributeValue.Add(attributeValue3b);
			
			// Brand
			var brand = new Brand { BrandName = "Armani" };
			Brand.Add(brand);

			// Category
			var category1 = new Category { CategoryName = "Category 1", IsPrimary = true };
			var category2 = new Category { CategoryName = "Category 2" };			
			Category.Add(category1);
			Category.Add(category2);			
			SaveChanges();

			// Product
			var product = new Product {
				ProductCode = "010101",
				ProductName = "Anita",
				ProductUm = "MT",
				ProductPrice = 10.0,
				BrandId = brand.BrandId
			};
			Product.Add(product);
			SaveChanges();

			// ProductCategory
			var productCategory1 = new ProductCategory {
				ProductId = product.ProductId,
				CategoryId = category1.CategoryId
			};
			var productCategory2 = new ProductCategory {
				ProductId = product.ProductId,
				CategoryId = category2.CategoryId
			};
			ProductCategory.Add(productCategory1);
			ProductCategory.Add(productCategory2);
			
			// ProductAttribute
			ProductAttribute.Add(new ProductAttribute {
				ProductId = product.ProductId,
				AttributeId = attribute0.AttributeId
			});
			ProductAttribute.Add(new ProductAttribute {
				ProductId = product.ProductId,
				AttributeId = attribute1.AttributeId
			});
			ProductAttribute.Add(new ProductAttribute {
				ProductId = product.ProductId,
				AttributeId = attribute2.AttributeId
			});
			ProductAttribute.Add(new ProductAttribute {
				ProductId = product.ProductId,
				AttributeId = attribute3.AttributeId
			});

			// ProductAttributeValue
			ProductAttributeValue.Add(new ProductAttributeValue {
				ProductId = product.ProductId,
				AttributeValueId = attributeValue0.AttributeValueId
			});
			ProductAttributeValue.Add(new ProductAttributeValue {
				ProductId = product.ProductId,
				AttributeValueId = attributeValue1.AttributeValueId
			});
			ProductAttributeValue.Add(new ProductAttributeValue {
				ProductId = product.ProductId,
				AttributeValueId = attributeValue2.AttributeValueId
			});
			ProductAttributeValue.Add(new ProductAttributeValue {
				ProductId = product.ProductId,
				AttributeValueId = attributeValue3a.AttributeValueId
			});
			ProductAttributeValue.Add(new ProductAttributeValue {
				ProductId = product.ProductId,
				AttributeValueId = attributeValue3b.AttributeValueId
			});
			
			// Article
			var article0 = new Article { ProductId = product.ProductId, Barcode = "01010101" };
			Article.Add(article0);
			var article1 = new Article { ProductId = product.ProductId, Barcode = "02020202" };
			Article.Add(article1);
			SaveChanges();

			// ArticleAttributeValue
			// Article 0
			ArticleAttributeValue.Add(new ArticleAttributeValue {
				AttributeId = attribute0.AttributeId,
				ArticleId = article0.ArticleId,
				AttributeValueId = attributeValue0.AttributeValueId
			});
			ArticleAttributeValue.Add(new ArticleAttributeValue {
				AttributeId = attribute1.AttributeId,
				ArticleId = article0.ArticleId,
				AttributeValueId = attributeValue1.AttributeValueId
			});
			ArticleAttributeValue.Add(new ArticleAttributeValue {
				AttributeId = attribute2.AttributeId,
				ArticleId = article0.ArticleId,
				AttributeValueId = attributeValue2.AttributeValueId
			});
			ArticleAttributeValue.Add(new ArticleAttributeValue {
				AttributeId = attribute3.AttributeId,
				ArticleId = article0.ArticleId,
				AttributeValueId = attributeValue3a.AttributeValueId
			});
			// Article 1
			ArticleAttributeValue.Add(new ArticleAttributeValue {
				AttributeId = attribute0.AttributeId,
				ArticleId = article1.ArticleId,
				AttributeValueId = attributeValue0.AttributeValueId
			});
			ArticleAttributeValue.Add(new ArticleAttributeValue {
				AttributeId = attribute1.AttributeId,
				ArticleId = article1.ArticleId,
				AttributeValueId = attributeValue1.AttributeValueId
			});
			ArticleAttributeValue.Add(new ArticleAttributeValue {
				AttributeId = attribute2.AttributeId,
				ArticleId = article1.ArticleId,
				AttributeValueId = attributeValue2.AttributeValueId
			});
			ArticleAttributeValue.Add(new ArticleAttributeValue {
				AttributeId = attribute3.AttributeId,
				ArticleId = article1.ArticleId,
				AttributeValueId = attributeValue3b.AttributeValueId
			});
			SaveChanges();
		}
    }
}