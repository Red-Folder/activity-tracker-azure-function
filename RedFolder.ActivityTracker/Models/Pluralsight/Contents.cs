using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedFolder.ActivityTracker.Models.Pluralsight
{
    public class Contents
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("courseId")]
        public string CourseId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("shortDescription")]
        public string ShortDescription { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("courseImageUrl")]
        public string CourseImageUrl { get; set; }

    }
}
