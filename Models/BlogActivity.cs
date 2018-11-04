using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Red_Folder.ActivityTracker.Models
{
    public class BlogActivity
    {
        [JsonProperty("blogs")]
        public List<Blog> Blogs { get; set; }
    }
}
