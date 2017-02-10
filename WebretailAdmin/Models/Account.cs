using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Webretail.Admin.Models
{
    [Table("account")]
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("account_id")]
        [JsonProperty("accountId")]
        public int AccountId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("account_name")]
        [JsonProperty("accountName")]
        public string AccountName { get; set; }

        [Required]
        [StringLength(100)]
        [Column("account_lastname")]
        [JsonProperty("accountLastname")]
        public string AccountLastname { get; set; }

        [Required]
        [StringLength(200)]
        [Column("account_email")]
        [JsonProperty("accountEmail")]
        public string AccountEmail { get; set; }

        [Required]
        [StringLength(200)]
        [Column("account_password")]
        [JsonProperty("accountPassword")]
        public string AccountPassword { get; set; }

        [Required]
        [Column("is_admin")]
        [JsonProperty("isAdmin")]
        public bool IsAdmin { get; set; }

		[Required]
		[JsonIgnore]
		[JsonProperty("created")]
		public DateTime Created { get; set; } = DateTime.Now;

		[Required]
		[Column("updated")]
		[JsonProperty("updated")]
		public DateTime Updated { get; set; } = DateTime.Now;
    }
}
