using Newtonsoft.Json;

namespace Red_Folder.ActivityTracker.Models
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
