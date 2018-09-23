using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Red_Folder.ActivityTracker.Models
{
    public class ClientActivity
    {
        [JsonProperty("clients")]
        public List<Client> Clients { get; set; }
    }
}
