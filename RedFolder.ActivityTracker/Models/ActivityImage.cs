using System;
using System.Collections.Generic;
using System.Text;

namespace RedFolder.ActivityTracker.Models
{
    public class ActivityImage
    {
        public Week Week { get; set; }
        public byte[] ImageData { get; set; }
    }
}
