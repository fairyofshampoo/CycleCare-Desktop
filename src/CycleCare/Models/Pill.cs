using Newtonsoft.Json;

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
