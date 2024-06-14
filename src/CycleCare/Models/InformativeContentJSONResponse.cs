using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycleCare.Models
{
    public class InformativeContentJSONResponse
    {

        [JsonProperty("contentId")]
        public int contentId { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty ("description")]
        public string description { get; set; }

        [JsonProperty ("creationDate")]
        public string creationDate { get; set; }

        [JsonProperty ("media")]
        public string media { get; set; }

        [JsonProperty ("username")]
        public string username { get; set; }


    }
}
