using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycleCare.Models
{
    public class ChangePasswordRequest
    {
        [JsonProperty("newPassword")]
        public string NewPassword { get; set; }
        [JsonProperty("confirmPassword")]
        public string ConfirmPassword { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
