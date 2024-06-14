using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycleCare.Models
{
    public class VaginalFlow
    {
        [JsonProperty("vaginalFlowId")]
        public int VaginalFlowId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
