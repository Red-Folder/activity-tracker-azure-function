using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedFolder.ActivityTracker.Models.Pluralsight
{
    public class Author
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("handle")]
        public string Handle { get; set; }
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
    }
}
