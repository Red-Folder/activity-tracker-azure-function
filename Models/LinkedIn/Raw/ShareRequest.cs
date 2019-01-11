using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Red_Folder.ActivityTracker.Models.LinkedIn.Raw
{
    public class ShareRequest
    {
        public ShareRequest()
        {
            LifecycleState = "PUBLISHED";
            SpecificContent = new ShareContent();
            Visibility = "PUBLIC";
        }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("lifecycleState")]
        public string LifecycleState { get; }

        [JsonProperty("specificContent")]
        public ShareContent SpecificContent { get; }

        [JsonProperty("visibility")]
        public string Visibility { get; }
    }
}
