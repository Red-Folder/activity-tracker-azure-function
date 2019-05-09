using System;
using System.Collections.Generic;

namespace RedFolder.ActivityTracker.Models.BlogService
{
    public class Blog
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public DateTime Published { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
        public List<string> KeyWords { get; set; }
    }
}
