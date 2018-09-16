﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Red_Folder.ActivityTracker.Models.Toggl
{
    public class DetailedReport
    {
        [JsonProperty("total_count")]
        public long TotalCount { get; set; }

        [JsonProperty("per_page")]
        public long PerPage { get; set; }

        [JsonIgnore]
        public int Pages
        {
            get
            {
                return (int)(TotalCount / PerPage);
            }
        }

        [JsonProperty("data")]
        public TimeEntry[] TimeEntries { get; set; }
    }
}
