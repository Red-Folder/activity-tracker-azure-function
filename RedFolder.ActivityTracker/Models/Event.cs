using Newtonsoft.Json;

namespace RedFolder.ActivityTracker.Models
{
    public class Event
    {
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
