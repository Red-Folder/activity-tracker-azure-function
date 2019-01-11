using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Red_Folder.ActivityTracker.Models.LinkedIn.Raw
{
    public class ShareMedia
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("media")]
        public string Media { get; set; }
        [JsonProperty("originalUrl")]
        public string OriginalUrl { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
