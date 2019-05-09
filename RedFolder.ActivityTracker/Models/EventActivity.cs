using Newtonsoft.Json;
using System.Collections.Generic;

namespace RedFolder.ActivityTracker.Models
{
    public class EventActivity
    {
        [JsonProperty("events")]
        public List<Event> Events;
    }
}
