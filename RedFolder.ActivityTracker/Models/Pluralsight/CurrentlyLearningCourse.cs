using Newtonsoft.Json;
using System;

namespace RedFolder.ActivityTracker.Models.Pluralsight
{
    public class CurrentlyLearningCourse: BaseCourse
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
        [JsonProperty("percentComplete")]
        public float PercentageComplete { get; set; }

        public override string Id => CourseId;

        public override bool IsWithinRange(DateTime start, DateTime end)
        {
            return LastViewedTimestamp >= start && LastViewedTimestamp <= end;
        }
    }
}
