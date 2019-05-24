using System;

namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class CategoryHandler : IHandler
    {
        private readonly string _feedname;
        private readonly string _category;
        private IHandler _inner;

        public CategoryHandler(string category, string feedname)
        {
            if (string.IsNullOrEmpty(category)) throw new NullReferenceException(nameof(category));
            if (string.IsNullOrEmpty(feedname)) throw new NullReferenceException(nameof(feedname));

            _feedname = feedname;
            _category = category;
        }

        public void AddInner(IHandler inner)
        {
            _inner = inner;
        }

        public Models.PodCast Convert(Models.BeyondPod.PodCastTableEntity source)
        {
            if (_inner == null) return null;

            var podcast = _inner.Convert(source);

            if (podcast.FeedName.Equals(_feedname, StringComparison.CurrentCultureIgnoreCase))
            {
                podcast.Category = _category;
                PostConvertActions(source, podcast);
            }

            return podcast;
        }

        protected virtual void PostConvertActions(Models.BeyondPod.PodCastTableEntity source, Models.PodCast destination)
        {
        }
    }
}
