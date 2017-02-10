using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Webretail.Admin.Models
{
    [Table("product_attribute_value")]
    public class ProductAttributeValue
    {
        [Required]
        [Column("product_id")]
        [JsonIgnore]
        public int ProductId { get; set; }

        [JsonIgnore]
        public Product Product { get; set; }

        [Required]
        [Column("attribute_value_id")]
        [JsonIgnore]
        public int AttributeValueId { get; set; }

        [JsonProperty("attributeValue")]
        public AttributeValue AttributeValue { get; set; }
    }
}
