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
        [JsonProperty("percentComplete")]
        public float PercentageComplete { get; set; }

        public override bool IsWithinRange(DateTime start, DateTime end)
        {
            return LastViewedTimestamp >= start && LastViewedTimestamp <= end;
        }
    }
}
