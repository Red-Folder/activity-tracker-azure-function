using System;
using System.Collections.Generic;
using System.Text;

namespace Red_Folder.ActivityTracker.Models
{
    public class ActivityImage
    {
        public Week Week { get; set; }
        public byte[] ImageData { get; set; }
    }
}
