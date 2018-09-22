using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Red_Folder.ActivityTracker.Models.Pluralsight
{
    public class CompletedCourse: BaseCourse
    {
        [JsonProperty("timeCompleted")]
        public DateTime TimeCompleted { get; set; }

        public override bool IsWithinRange(DateTime start, DateTime end)
        {
            return TimeCompleted >= start && TimeCompleted <= end;
        }
    }
}
