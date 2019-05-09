using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedFolder.ActivityTracker.Models.LinkedIn
{
    public class ShareRequest
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }
    }
}
