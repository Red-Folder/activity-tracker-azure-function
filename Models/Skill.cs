using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Red_Folder.ActivityTracker.Models
{
    public class Skill
    {
        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("totalDuration")]
        public long TotalDuration { get; set; }
    }
}
