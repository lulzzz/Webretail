using Newtonsoft.Json;
using Webretail.Admin.Helpers;

namespace Webretail.Admin.Models
{
    public class LoginModel
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        public string PasswordCrypted => CryptHelper.SHA1HashStringForUTF8String(Password);
    }
}