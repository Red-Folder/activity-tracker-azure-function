using Newtonsoft.Json;

namespace RedFolder.ActivityTracker.Models
{
    public class WeeklyActivityToBeTweeted
    {
        [JsonProperty("year")]
        public int Year { get; set; }
        [JsonProperty("weekNumber")]
        public int WeekNumber { get; set; }
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }
    }
}
