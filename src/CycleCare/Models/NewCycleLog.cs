using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CycleCare.Models
{
    public class NewCycleLog
    {
        [JsonProperty("cycleLogId")]
        public int CycleLogId { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("sleepHours")]
        public int SleepHours { get; set; }

        [JsonProperty("creationDate")]
        public DateTime CreationDate { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("menstrualFlowId")]
        public int MenstrualFlowId { get; set; }

        [JsonProperty("vaginalFlowId")]
        public int VaginalFlowId { get; set; }

        [JsonProperty("symptoms")]
        public List<Symptom> Symptoms { get; set; }

        [JsonProperty("moods")]
        public List<Mood> Moods { get; set; }

        [JsonProperty("medications")]
        public List<Medication> Medications { get; set; }

        [JsonProperty("pills")]
        public List<Pill> Pills { get; set; }

        [JsonProperty("birthControls")]
        public List<BirthControl> BirthControls { get; set; }
    }
}
