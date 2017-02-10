using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Webretail.Admin.Models
{
    [Table("article_attribute_value")]
    public class ArticleAttributeValue
    {
        [Required]
        [Column("article_id")]
        [JsonIgnore]
        public long ArticleId { get; set; }

        [JsonIgnore]
        public Article article { get; set; }

        [Required]
        [Column("attribute_id")]
        [JsonIgnore]
        public int AttributeId { get; set; }

        [Required]
        [Column("attribute_value_id")]
        [JsonIgnore]
        public int AttributeValueId { get; set; }

        [JsonProperty("attributeValue")]
        public AttributeValue AttributeValue { get; set; }
    }
}
