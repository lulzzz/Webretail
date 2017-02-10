using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Webretail.Admin.Models
{
    [Table("attribute_value")]
    public class AttributeValue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("attribute_value_id")]
        [JsonProperty("attributeValueId")]
        public int AttributeValueId { get; set; }

        [Required]
        [Column("attribute_value_code")]
        [JsonProperty("attributeValueCode")]
        public string AttributeValueCode { get; set; }

        [Required]
        [Column("attribute_value_name")]
        [JsonProperty("attributeValueName")]
        public string AttributeValueName { get; set; }

		[Required]
        [Column("attribute_id")]
        [JsonProperty("attributeId")]
        public int AttributeId { get; set; }

        [JsonIgnore]
        public Attribute Attribute { get; set; }

		[Required]
		[Column("created")]
		[JsonIgnore]
		public DateTime Created { get; set; } = DateTime.Now;

		[Required]
        [Column("updated")]
        [JsonProperty("updated")]
        public DateTime Updated { get; set; } = DateTime.Now;
    }
}
