using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedFolder.ActivityTracker.Models
{
    public class PluralsightActivity
    {
        [JsonProperty("courses")]
        public List<Course> Courses;
    }
}
