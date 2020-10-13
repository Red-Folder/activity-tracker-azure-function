using System;

namespace RedFolder.ActivityTracker.Models.Pluralsight
{
    public abstract class BaseCourse
    {
        public abstract string Id { get; }
        public abstract bool IsWithinRange(DateTime start, DateTime end);
    }
}
