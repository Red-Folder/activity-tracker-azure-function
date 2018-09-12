#r "Microsoft.WindowsAzure.Storage"
#r "Newtonsoft.Json"

using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;

public static async Task Run(CloudBlockBlob source, string name, CloudTable destination, TraceWriter log)
{
    log.Info($"Processing Beyond Pod data in:{name}");

    log.Info($"Retrieving json from source");
    var json = await source.DownloadTextAsync();
    var beyondPod = JsonConvert.DeserializeObject<BeyondPod>(json);

    var partitionKey = ToAzureKeyString(beyondPod.FeedName);
    var rowKey = ToAzureKeyString(beyondPod.EpisodeName);
    var retrieveOperation = TableOperation.Retrieve<PodCast>(partitionKey, rowKey);

    PodCast podCast = null;

    log.Info($"Trying to load existing podcast record");
    var query = await destination.ExecuteAsync(retrieveOperation);
    if (query.Result == null)
    {
        log.Info("Record does not exist, to create");
        podCast = new PodCast(partitionKey, rowKey);

        // TODO Set all the properties
    }
    else
    {
        log.Info("Record exists, to update");
        podCast = (PodCast)query.Result;

        // TODO if Created later, then update position, playing & created
    }

    // TODO Remove this
    podCast.BeyondPodEntries.Add(new BeyondPodEntries {
        Position = beyondPod.Position
    });

    log.Info("Saving podcast");
    var saveOperation = TableOperation.InsertOrMerge(podCast);
    await destination.ExecuteAsync(saveOperation);

    log.Info("Deleting source");
    //source.Delete();
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
    [JsonProperty(PropertyName = "feedName")]
    public string FeedName { get; set; }

    [JsonProperty(PropertyName = "episodeName")]
    public string EpisodeName { get; set; }

    [JsonProperty(PropertyName = "episodePosition")]
    public long Position { get; set; }

    // TODO add all properties
}

public class PodCast: TableEntity
{
    public PodCast() : base ()
    {
        // TODO remove this
        BeyondPodEntries = new List<BeyondPodEntries>();
    }

    public PodCast(string feedName, string episodeName): base(feedName, episodeName)
    {
        // TODO remove this
        BeyondPodEntries = new List<BeyondPodEntries>();
    }

        // TODO remove this
    public List<BeyondPodEntries> BeyondPodEntries { get; set; }

    // TODO add all properties
}

        // TODO remove this
public class BeyondPodEntries
{
    public long Position { get; set; }
}