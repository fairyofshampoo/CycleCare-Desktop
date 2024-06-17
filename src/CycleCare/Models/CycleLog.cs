using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycleCare.Models
{
    public class CycleLog
    {
        [JsonProperty("statusCode")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Details { get; set; }

        [JsonProperty("cycleLogId")]
        public int CycleLogId { get; set; }

        [JsonProperty("creationDate")]
        public DateTime CreationDate { get; set; }

        [JsonProperty("menstrualFlow")]
        public MenstrualFlow MenstrualFlow { get; set; }

        [JsonProperty("vaginalFlow")]
        public VaginalFlow VaginalFlow { get; set; }

        [JsonProperty("sleepHours")]
        public int SleepHours { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("symptoms")]
        public List<Symptom> Symptoms { get; set; }

        [JsonProperty("moods")]
        public List<Mood> Moods { get; set; }

        [JsonProperty("medications")]
        public List<Medication> Medications { get; set; }

        [JsonProperty("pills")]
        public List<Pill> Pills { get; set; }

        [JsonProperty("birthControl")]
        public List<BirthControl> BirthControl { get; set; }
    }
}
