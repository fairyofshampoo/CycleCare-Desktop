using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycleCare.Models
{
    public class SleepHours
    {
        [JsonProperty("dayOfWeek")]
        public string dayOfWeek { get; set; }

        [JsonProperty("totalSleepHours")]
        public string totalSleepHours { get; set; }
    }
}
