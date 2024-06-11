using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CycleCare.Models
{
    public class CycleLog
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

        [JsonProperty("menstrualFlow")]
        public MenstrualFlow MenstrualFlow { get; set; }

        [JsonProperty("vaginalFlow")]
        public VaginalFlow VaginalFlow { get; set; }

        [JsonProperty("symptoms")]
        public List<Symptom> Symptoms { get; set; }

        [JsonProperty("moods")]
        public List<Mood> Moods { get; set; }

        [JsonProperty("medications")]
        public List<Medication> Medications { get; set; }

        [JsonProperty("pills")]
        public List<Pill> Pills { get; set; }

        [JsonProperty("birthControl")]
        public BirthControl BirthControl { get; set; }
    }
}
