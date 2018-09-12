#r "Microsoft.WindowsAzure.Storage"
//#r "Newtonsoft.Json"

using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
//using Newtonsoft.Json;
using System.Text;

public static async Task Run(BeyondPod source, CloudTable destination, ICollector<PodCast> podcastReadyToGo, TraceWriter log)
{
    log.Info($"Processing Beyond Pod data");

//    log.Info($"Retrieving json from source");
//    var json = await source.DownloadTextAsync();
//    var beyondPod = JsonConvert.DeserializeObject<BeyondPod>(json);

    var partitionKey = ToAzureKeyString(source.FeedName);
    var rowKey = ToAzureKeyString(source.EpisodeName);
    var retrieveOperation = TableOperation.Retrieve<PodCast>(partitionKey, rowKey);

    log.Info($"Trying to load existing podcast record");
    var query = await destination.ExecuteAsync(retrieveOperation);
    var isNew = (query.Result == null);

    PodCast podCast = isNew ? NewPodCast(partitionKey, rowKey, source): UpdatePodCast((PodCast)query.Result, source);

    podCast = ApplyFixes(podCast);

    var shouldBeProgressed = ShouldPodCastBeProgressed(podCast);

    if (shouldBeProgressed)
    {
        podCast.Actioned = true;
    }

    // Make sure we can save the podcast before we progress
    await Save(destination, isNew, podCast);

    if (shouldBeProgressed)
    {
        podcastReadyToGo.Add(podCast); 
    }
}

public static PodCast NewPodCast(String partitionKey, String rowKey, BeyondPod source)
{
    var podCast = new PodCast(partitionKey, rowKey);

    podCast.Created = source.Created;
    podCast.Playing = source.Playing;
    podCast.FeedName = source.FeedName;
    podCast.FeedUrl = source.FeedUrl;
    podCast.EpisodeName = source.EpisodeName;
    podCast.EpisodeUrl = source.EpisodeUrl;
    podCast.EpisodeFile = source.EpisodeFile;
    podCast.EpisodePostUrl = source.EpisodePostUrl;
    podCast.EpisodeMime = source.EpisodeMime;
    podCast.EpisodeSummary = source.EpisodeSummary;
    podCast.EpisodeDuration = source.EpisodeDuration;
    podCast.EpisodePosition = source.EpisodePosition;
    podCast.Artist = source.Artist;
    podCast.Album = source.Album;
    podCast.Track = source.Track;

    return podCast;
}

public static PodCast UpdatePodCast(PodCast podCast, BeyondPod source)
{
    if (source.Created > podCast.Created) {
       podCast.Created = source.Created;
       podCast.Playing = source.Playing;
       podCast.EpisodePosition = source.EpisodePosition;

       // Sometimes we seem to see zero durations
       if (podCast.EpisodeDuration < source.EpisodeDuration) {
          podCast.EpisodeDuration = source.EpisodeDuration;
       }
    }

    return podCast;
}

public static async Task Save(CloudTable destination, bool isNew, PodCast podCast) 
{
    if (isNew) 
    {
        var insertOperation = TableOperation.Insert(podCast);
        await destination.ExecuteAsync(insertOperation);    
    } else {
        var replaceOperation = TableOperation.Replace(podCast);
        await destination.ExecuteAsync(replaceOperation);    
    }
}

public static PodCast ApplyFixes(PodCast podCast)
{
    // Apply fix for when duration is less than position
    if (podCast.EpisodeDuration < podCast.EpisodePosition) podCast.EpisodeDuration = podCast.EpisodePosition;

    if (podCast.FeedName == "JavaScript Jabber Only") podCast.FeedName = "JavaScript Jabber";
    if (podCast.FeedName == "Adventures in Angular Only") podCast.FeedName = "Adventures in Angular";

    return podCast;
}

public static bool ShouldPodCastBeProgressed(PodCast podCast)
{
    if (podCast.Actioned) return false;
    if (podCast.EpisodeDuration == 0) return false;

    var percentageThrough = (float)podCast.EpisodePosition/ (float)podCast.EpisodeDuration;
    
    if (percentageThrough <= 0.9) return false;

    return true;
}

public static string ToAzureKeyString(string str)
{
    var sb = new StringBuilder();
    foreach (var c in str
        .Where(c => c != '/'
                    && c != '\\'
                    && c != '#'
                    && c != '/'
                    && c != '?'
                    && !char.IsControl(c)))
        sb.Append(c);
    return sb.ToString();
}

public class BeyondPod 
{
    //[JsonProperty(PropertyName = "feedName")]
    public DateTime Created { get; set; }
    public bool Playing { get; set; }
    public string FeedName { get; set; }
    public string FeedUrl { get; set; }
    public string EpisodeName { get; set; }
    public string EpisodeUrl { get; set; }
    public string EpisodeFile { get; set; }
    public string EpisodePostUrl { get; set; }
    public string EpisodeMime { get; set; }
    public string EpisodeSummary { get; set; }
    public long EpisodeDuration { get; set; }
    public long EpisodePosition { get; set; }
    public string Artist { get; set; }
    public string Album { get; set; }
    public string Track { get; set; }
}

public class PodCast: TableEntity
{
    public PodCast() : base ()
    {
    }

    public PodCast(string partitionKey, string rowKey): base(partitionKey, rowKey)
    {
    }

    public DateTime Created { get; set; }
    public bool Playing { get; set; }
    public string FeedName { get; set; }
    public string FeedUrl { get; set; }
    public string EpisodeName { get; set; }
    public string EpisodeUrl { get; set; }
    public string EpisodeFile { get; set; }
    public string EpisodePostUrl { get; set; }
    public string EpisodeMime { get; set; }
    public string EpisodeSummary { get; set; }
    public long EpisodeDuration { get; set; }
    public long EpisodePosition { get; set; }
    public string Artist { get; set; }
    public string Album { get; set; }
    public string Track { get; set; }

    public bool Actioned { get; set; }
}

