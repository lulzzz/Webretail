using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Webretail.Admin.Models
{
    [Table("attribute")]
    public class Attribute
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("attribute_id")]
        [JsonProperty("attributeId")]
        public int AttributeId { get; set; }

        [Required]
        [Column("attribute_name")]
        [JsonProperty("attributeName")]
        public string AttributeName { get; set; }


		[Required]
		[JsonIgnore]
		[JsonProperty("created")]
		public DateTime Created { get; set; } = DateTime.Now;

		[Required]
		[Column("updated")]
		[JsonProperty("updated")]
		public DateTime Updated { get; set; } = DateTime.Now;


		[JsonIgnore]
		public virtual ICollection<AttributeValue> Values { get; set; }
    }
}
