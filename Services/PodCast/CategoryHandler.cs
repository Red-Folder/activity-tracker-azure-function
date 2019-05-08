using Red_Folder.ActivityTracker.Models;
using System;

namespace Red_Folder.ActivityTracker.Services.PodCast
{
    public class CategoryHandler : IHandler
    {
        private readonly string _feedname;
        private readonly string _category;
        private IHandler _inner;

        public CategoryHandler(string category, string feedname)
        {
            _feedname = feedname;
            _category = category;
        }

        public void AddInner(IHandler inner)
        {
            _inner = inner;
        }

        public Models.PodCast Convert(PodCastTableEntity source)
        {
            if (_inner == null) return null;

            var podcast = _inner.Convert(source);

            if (podcast.FeedName.Equals(_feedname, StringComparison.CurrentCultureIgnoreCase))
            {
                podcast.Category = _category;
            }

            return podcast;
        }
    }
}
