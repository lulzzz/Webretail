using System;
using Newtonsoft.Json;

namespace Webretail.Admin.Models
{
    public class TokenModel
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("expiry")]
        public DateTime Expiry { get; set; }
    }
}