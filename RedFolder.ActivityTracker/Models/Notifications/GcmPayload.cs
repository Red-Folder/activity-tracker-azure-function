using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedFolder.ActivityTracker.Models.Notifications
{
    public class GcmPayload
    {
        [JsonProperty("notification")]
        public Notification Payload { get; set; }

        public class Notification
        {
            [JsonProperty("title")]
            public string Title { get; set; }
            [JsonProperty("body")]
            public string Message { get; set; }
            [JsonProperty("click_action")]
            public string ClickAction { get; set; }
            [JsonProperty("icon")]
            public string IconUrl { get; set; }
        }
    }
}
