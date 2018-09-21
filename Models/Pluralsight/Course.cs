using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Red_Folder.ActivityTracker.Models.Pluralsight
{
    public class Course
    {
        [JsonProperty("courseId")]
        public string CourseId { get; set; }
        [JsonProperty("courseName")]
        public string CourseName { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("authors")]
        public Author[] Authors { get; set; }
        [JsonProperty("level")]
        public string Level;
        [JsonProperty("duration")]
        public TimeSpan Duration;
        [JsonProperty("lastViewedTimestamp")]
        public DateTime LastViewedTimestamp { get; set; }
        [JsonProperty("percentageComplete")]
        public float PercentageComplete { get; set; }
    }
}
