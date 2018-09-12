#r "Microsoft.WindowsAzure.Storage"

using Microsoft.WindowsAzure.Storage.Queue;
using System.Net;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, ICollector<string> outputQueueItem, TraceWriter log)
{
    log.Info("BeyondPodRawData function triggered");

    var jsonString = await req.Content.ReadAsStringAsync();
    if (String.IsNullOrEmpty(jsonString)) 
    {
        log.Warning("Received empty string");
        return req.CreateResponse(HttpStatusCode.BadRequest, "Expecting json payload");
    }

    log.Info($"Received: {jsonString}");
    outputQueueItem.Add(jsonString); 

    return req.CreateResponse(HttpStatusCode.Created);        
}
