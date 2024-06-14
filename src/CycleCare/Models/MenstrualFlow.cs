using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycleCare.Models
{
    public class MenstrualFlow
    {
        [JsonProperty("menstrualFlowId")]
        public int MenstrualFlowId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

    }
}
