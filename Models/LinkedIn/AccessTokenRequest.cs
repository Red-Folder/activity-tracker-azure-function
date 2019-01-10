using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Red_Folder.ActivityTracker.Models.LinkedIn
{
    public class AccessTokenRequest
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("redirectUri")]
        public string RedirectUri { get; set; }
    }
}
