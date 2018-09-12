using System.Net;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info("Ping called");

    return req.CreateResponse(HttpStatusCode.OK);
}
