using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycleCare.Models
{
    public class ArticleUpdated
    {

        [JsonProperty ("contentId")]
        public int contentId { get; set; }

        [JsonProperty ("title")]
        public string title { get; set; }

        [JsonProperty ("description")]
        public string description { get; set; }

        [JsonProperty ("imageName")]
        public string imageName { get; set; }

        [JsonProperty ("newImage")]
        public string newImage { get; set; }

    }
}
