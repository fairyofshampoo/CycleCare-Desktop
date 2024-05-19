using Newtonsoft.Json;

namespace CycleCare.Models
{
    public class Response
    {
        [JsonProperty("statusCode")]
        public int Code { get; set; }

        [JsonProperty("details")]
        public string Details { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("firstLastName")]
        public string FirstLastName { get; set; }

        [JsonProperty("secondLastName")]
        public string SecondLastName { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }
    }
}