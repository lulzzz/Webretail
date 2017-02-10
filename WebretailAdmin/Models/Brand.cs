using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Webretail.Admin.Models
{
    [Table("brand")]
    public class Brand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("brand_id")]
        [JsonProperty("brandId")]
        public int BrandId { get; set; }

        [Required]
        [StringLength(200)]
        [Column("brand_name")]
        [JsonProperty("brandName")]
        public string BrandName { get; set; }

		[Required]
		[Column("created")]
		[JsonIgnore]
		public DateTime Created { get; set; } = DateTime.Now;

		[Required]
		[Column("updated")]
		[JsonProperty("updated")]
		public DateTime Updated { get; set; } = DateTime.Now;


		[JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}
