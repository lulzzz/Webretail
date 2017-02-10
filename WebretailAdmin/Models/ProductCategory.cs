using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Webretail.Admin.Models
{
    [Table("product_category")]
    public class ProductCategory
    {
		[Required]
		[Column("product_id")]
		[JsonIgnore]
		public int ProductId { get; set; }

		[JsonIgnore]
		public Product Product { get; set; }

		[Required]
		[Column("category_id")]
		[JsonIgnore]
		public int CategoryId { get; set; }

		[JsonProperty("category")]
		public Category Category { get; set; }
    }
}
