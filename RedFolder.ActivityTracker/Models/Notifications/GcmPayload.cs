using Newtonsoft.Json;

namespace RedFolder.ActivityTracker.Models.Notifications
{
    public class FcmPayload
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
