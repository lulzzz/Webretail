using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Webretail.Admin.Models
{
    [Table("article")]
    public class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("article_id")]
        [JsonProperty("articleId")]
        public long ArticleId { get; set; }

		[Required]
        [Column("barcode")]
        [JsonProperty("barcode")]
        public string Barcode { get; set; }

		[Required]
        [Column("product_id")]
        [JsonIgnore]
        public int ProductId { get; set; }

        [JsonIgnore]
        public Product Product { get; set; }

		[Required]
		[Column("created")]
		[JsonIgnore]
        public DateTime Created { get; set; } = DateTime.Now;

		[Required]
		[Column("updated")]
		[JsonProperty("updated")]
		public DateTime Updated { get; set; } = DateTime.Now;


		[JsonProperty("attributeValues")]
		public virtual ICollection<ArticleAttributeValue> AttributeValues { get; set; }
    }
}
