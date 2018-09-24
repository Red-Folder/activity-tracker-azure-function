using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Red_Folder.ActivityTracker.Models.Pluralsight
{
    public class Contents
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("courseImageUrl")]
        public string CourseImageUrl { get; set; }
    }
}
