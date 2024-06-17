using Newtonsoft.Json;
using System.Collections.Generic;

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

        [JsonProperty("reminders")]
        public List<Reminder> Reminders { get; set; }

        [JsonProperty("cycleLogs")]
        public List<CycleLog> CycleLogs { get; set; }

        [JsonProperty("InformativeContent")]
        public List<InformativeContentJSONResponse> contents { get; set; }

        [JsonProperty("SleepHours")]
        public List<SleepHours> sleepHours { get; set; }    
    }
}