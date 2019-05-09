using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace RedFolder.ActivityTracker.Models
{
    public class Blog
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("published")]
        public DateTime Published { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("keyWords")]
        public List<string> KeyWords { get; set; }
    }
}
