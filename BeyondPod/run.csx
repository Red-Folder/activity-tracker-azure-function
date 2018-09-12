using System.Net;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TextWriter outputBlob, TraceWriter log)
{
    log.Info("BeyondPod function triggered");

    var jsonString = await req.Content.ReadAsStringAsync();
    if (String.IsNullOrEmpty(jsonString)) 
    {
        log.Warning("Received empty string");
        return req.CreateResponse(HttpStatusCode.BadRequest, "Expecting json payload");
    }

    log.Info($"Received: {jsonString}");
    outputBlob.Write(jsonString);

    return req.CreateResponse(HttpStatusCode.Created);
}
