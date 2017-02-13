using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Webretail.Admin.Models
{
    [Table("product_attribute")]
    public class ProductAttribute
    {
        [Required]
        [Column("product_id")]
		[JsonProperty("productId")]
        public int ProductId { get; set; }

        [JsonIgnore]
        public Product Product { get; set; }

        [Required]
        [Column("attribute_id")]
        [JsonIgnore]
        public int AttributeId { get; set; }

        [JsonProperty("attribute")]
        public Attribute Attribute { get; set; }
    }
}
