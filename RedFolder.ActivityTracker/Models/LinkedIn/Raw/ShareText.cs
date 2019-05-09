using Newtonsoft.Json;

namespace RedFolder.ActivityTracker.Models.LinkedIn.Raw
{
    public class ShareText
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
