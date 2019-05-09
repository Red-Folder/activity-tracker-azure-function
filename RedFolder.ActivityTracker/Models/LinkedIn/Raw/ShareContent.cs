using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedFolder.ActivityTracker.Models.LinkedIn.Raw
{
    public class ShareContent
    {
        public ShareContent()
        {
            ShareCommentary = "Test - please ignore";
            ShareMediaCategory = "NONE";
        }

        [JsonProperty("shareCommentary")]
        public string ShareCommentary { get; set; }

        [JsonProperty("shareMediaCategory")]
        public string ShareMediaCategory { get; }

        [JsonProperty("media")]
        public ShareMedia[] Media { get; set; }
    }
}
