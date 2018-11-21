using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.NotificationHubs;
using System;

namespace Red_Folder.ActivityTracker.Functions
{
    public static class RegisterForNotifications
    {
        [FunctionName("RegisterForNotifications")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            var handle = data.handle.Value;

            var connectionString = Environment.GetEnvironmentVariable("NotificationHubConnectionString", EnvironmentVariableTarget.Process);
            var hub = new NotificationHubClient(connectionString, "rfc-activity");

            CollectionQueryResult<RegistrationDescription> registrationForHandle = await hub.GetRegistrationsByChannelAsync(handle, 100);

            var enumerator = registrationForHandle.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                var registration = new GcmRegistrationDescription(handle);
                await hub.CreateRegistrationAsync(registration);
            }

            return new OkResult();
        }
    }
}
