using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycleCare.Models
{
    public class Pill
    {
        [JsonProperty("pillId")]
        public int PillId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
