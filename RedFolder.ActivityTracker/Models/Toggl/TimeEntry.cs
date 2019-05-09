using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedFolder.ActivityTracker.Models.Toggl
{
    public class TimeEntry
    {
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("start")]
        public DateTime Start { get; set; }
        [JsonProperty("end")]
        public DateTime End { get; set; }
        [JsonProperty("dur")]
        public long Duration { get; set; }
        [JsonProperty("client")]
        public string Client { get; set; }
        [JsonProperty("project")]
        public string Project { get; set; }

        [JsonProperty("tags")]
        public string[] tags { get; set; }
    }
}
