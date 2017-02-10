using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Webretail.Admin.Models
{
    [Table("publish")]
    public class Publish
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("publish_id")]
        [JsonProperty("publishId")]
        public int PublishId { get; set; }

		[Required]
		[Column("featured")]
		[JsonProperty("featured")]
		public bool Featured { get; set; }

		[Required]
        [Column("product_id")]
        [JsonProperty("productId")]
        public int ProductId { get; set; }

        [JsonProperty("product")]
        public Product Product { get; set; }

		[Required]
		[Column("start_at")]
		[JsonProperty("startAt")]
		public DateTime StartAt { get; set; }

		[Required]
		[Column("finish_at")]
		[JsonProperty("finishAt")]
		public DateTime FinishAt { get; set; }
	}
}
