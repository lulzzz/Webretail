using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace Webretail.Admin.Models
{
    [Table("product")]
    public class Product
    {
		[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("product_id")]
        [JsonProperty("productId")]
        public int ProductId { get; set; }

		[Required]
        [StringLength(10)]
        [Column("product_code")]
        [JsonProperty("productCode")]
        
        public string ProductCode { get; set; }
		[Required]
        [StringLength(100)]
        [Column("product_name")]
        [JsonProperty("productName")]
        public string ProductName { get; set; }

        [Required]
        [StringLength(2)]
        [Column("product_um")]
        [JsonProperty("productUm")]
        public string ProductUm { get; set; }

		[Required]
        [Column("product_price")]
        [JsonProperty("productPrice")]
        public double ProductPrice { get; set; }

		[Required]
        [Column("brand_id")]
        [JsonIgnore]
        public int BrandId { get; set; }

        [JsonProperty("brand")]
        public Brand Brand { get; set; }

		[Required]
		[Column("created")]
		[JsonIgnore]
		public DateTime Created { get; set; } = DateTime.Now;

		[Required]
        [Column("updated")]
        [JsonProperty("updated")]
        public DateTime Updated { get; set; } = DateTime.Now;


		[JsonProperty("category")]
        public virtual string Category => string.Join(",", Categories.Select(p => p.Category.CategoryName));

		[JsonProperty("categories")]
		public virtual ICollection<ProductCategory> Categories { get; set; }

		[JsonProperty("attributes")]
		public virtual ICollection<ProductAttribute> Attributes { get; set; }

		[JsonProperty("attributeValues")]
		public virtual ICollection<ProductAttributeValue> AttributeValues { get; set; }

		[JsonProperty("articles")]
		public virtual ICollection<Article> Articles { get; set; }
    }
}
