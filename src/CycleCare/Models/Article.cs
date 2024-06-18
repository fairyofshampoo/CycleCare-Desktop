using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycleCare.Models
{
    public class Article
    {
        [JsonProperty ("title")]
        public string title { get; set; }

        [JsonProperty ("description")]
        public string description { get; set; }

        [JsonProperty ("creationDate")]
        public string creationDate { get; set; }

        [JsonProperty ("image")]
        public string image { get; set; }

    }
}
