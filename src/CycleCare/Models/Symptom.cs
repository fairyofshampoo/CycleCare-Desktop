using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycleCare.Models
{
    public class Symptom
    {
        [JsonProperty("symptomId")]
        public int SymptomId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
