using Newtonsoft.Json;

namespace CycleCare.Models
{
    public class User
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}