namespace RedFolder.ActivityTracker.BeyondPod.Converters.Handlers
{
    public class DefaultCategoryHandler : IHandler
    {
        private IHandler _inner;

        public void AddInner(IHandler inner)
        {
            _inner = inner;
        }

        public Models.PodCast Convert(Models.BeyondPod.PodCastTableEntity source)
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
