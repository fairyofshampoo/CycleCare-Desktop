using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycleCare.Models
{
    public class Account
    {
        [JsonProperty ("name")]
        public string name { get; set; }

        [JsonProperty("firstLastName")]
        public string firstLastName { get; set; }

        [JsonProperty("secondLastName")]
        public string secondLastName { get; set; }
        
        [JsonProperty("email")]
        public string email { get; set; }
        
        [JsonProperty("username")]
        public string username { get; set; }
        
        [JsonProperty("password")]
        public string password { get; set; }
        
        [JsonProperty("role")]
        public string role { get; set; }
        
        [JsonProperty("isRegular")]
        public int isRegular { get; set; }
        
        [JsonProperty("aproxCycleDuration")]
        public int aproxCycleDuration { get; set; }
        
        [JsonProperty("aproxPeriodDuration")]
        public int aproxPeriodDuration { get; set; }

    }
}
