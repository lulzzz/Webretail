using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Webretail.Admin.Models
{
    [Table("category")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("category_id")]
        [JsonProperty("categoryId")]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(200)]
        [Column("category_name")]
        [JsonProperty("categoryName")]
        public string CategoryName { get; set; }

        [Required]
        [Column("is_primary")]
        [JsonProperty("isPrimary")]
        public bool IsPrimary { get; set; }

		[Required]
		[Column("created")]
		[JsonIgnore]
		public DateTime Created { get; set; } = DateTime.Now;

		[Required]
		[Column("updated")]
		[JsonProperty("updated")]
		public DateTime Updated { get; set; } = DateTime.Now;


		[JsonIgnore]
		public virtual ICollection<ProductCategory> Products { get; set; }
    }
}
