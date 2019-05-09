using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedFolder.ActivityTracker.Models.Pluralsight
{
    public abstract class BaseCourse
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

        public abstract bool IsWithinRange(DateTime start, DateTime end);
    }
}
