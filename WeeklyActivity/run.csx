using System.Net;
using System.Text;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, int? year, int? weekNumber, string weeklyActivity, TraceWriter log)
{
    log.Info($"Weekyl Activity requested for Year: {year}, Week Number: {weekNumber}");

    if (String.IsNullOrEmpty(weeklyActivity)) {
        return req.CreateResponse(HttpStatusCode.NotFound, $"Weekly activity for {year} & {weekNumber} not found");
    }

    return new HttpResponseMessage(HttpStatusCode.OK) {
        Content = new StringContent(weeklyActivity, Encoding.UTF8, "application/json")
    };
}
