using Newtonsoft.Json;
using System;

namespace RedFolder.ActivityTracker.Models.Pluralsight
{
    public class CompletedCourse: BaseCourse
    {
        [JsonProperty("contentId")]
        public string CourseId { get; set; }
        [JsonProperty("slug")]
        public string CourseName { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("authors")]
        public Author[] Authors { get; set; }
        [JsonProperty("level")]
        public string Level;

        [JsonProperty("timeCompleted")]
        public DateTime TimeCompleted { get; set; }

        public override string Id => CourseId;

        public override bool IsWithinRange(DateTime start, DateTime end)
        {
            return TimeCompleted >= start && TimeCompleted <= end;
        }
    }
}
