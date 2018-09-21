using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Red_Folder.ActivityTracker.Models.Pluralsight
{
    public class CurrentlyLearningCourse: BaseCourse
    {
        [JsonProperty("lastViewedTimestamp")]
        public DateTime LastViewedTimestamp { get; set; }
        [JsonProperty("percentageComplete")]
        public float PercentageComplete { get; set; }
    }
}
