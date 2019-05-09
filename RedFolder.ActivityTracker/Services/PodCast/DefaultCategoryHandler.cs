using RedFolder.ActivityTracker.Models;

namespace RedFolder.ActivityTracker.Services.PodCast
{
    public class DefaultCategoryHandler : IHandler
    {
        private IHandler _inner;

        public void AddInner(IHandler inner)
        {
            _inner = inner;
        }

        public Models.PodCast Convert(PodCastTableEntity source)
        {
            if (_inner == null) return null;

            var podcast = _inner.Convert(source);

            if (string.IsNullOrEmpty(podcast.Category))
            {
                podcast.Category = "Other";
            }

            return podcast;
        }
    }
}
