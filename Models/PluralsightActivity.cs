using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Red_Folder.ActivityTracker.Models
{
    public class PluralsightActivity
    {
        [JsonProperty("pluralsight")]
        public List<Course> Courses;
    }
}
