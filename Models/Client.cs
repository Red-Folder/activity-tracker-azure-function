using Newtonsoft.Json;

namespace Red_Folder.ActivityTracker.Models
{
    public class Client
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("totalDuration")]
        public long TotalDuration { get; set; }
    }
}
