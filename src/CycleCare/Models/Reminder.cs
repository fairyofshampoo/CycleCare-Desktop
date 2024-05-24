using Newtonsoft.Json;
using System;

namespace CycleCare.Models
{
    public class Reminder
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("creationDate")]
        public DateTime CreationDate { get; set; }

        [JsonProperty("reminderId")]
        public int Id { get; set; }

        [JsonProperty("scheduleId")]
        public string ScheduleId { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }
    }
}
