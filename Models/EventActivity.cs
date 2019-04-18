using Newtonsoft.Json;
using System.Collections.Generic;

namespace Red_Folder.ActivityTracker.Models
{
    public class EventActivity
    {
        [JsonProperty("events")]
        public List<Event> Events;
    }
}
