using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Red_Folder.ActivityTracker.Utilities;
using Red_Folder.ActivityTracker.Models.LinkedIn;
using Red_Folder.ActivityTracker.Services;

namespace Red_Folder.ActivityTracker.Functions
{
    public static class LinkedInTestPost
    {
        [FunctionName("LinkedInTestPost")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var authentication = new Authentication(req);

            if (authentication.Disabled || authentication.IsAuthorised)
            {
                try
                {
                    var json = await req.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<ShareRequest>(json);

                    var linkedInUserId = Environment.GetEnvironmentVariable("LinkedInUserId", EnvironmentVariableTarget.Process);
                    var linkedInClientId = Environment.GetEnvironmentVariable("LinkedInClientId", EnvironmentVariableTarget.Process);
                    var linkedInClientSecret = Environment.GetEnvironmentVariable("LinkedInClientSecret", EnvironmentVariableTarget.Process);

                    var proxy = new LinkedInProxy(linkedInUserId, linkedInClientId, linkedInClientSecret, log);

                    await proxy.CreateShare(data.AccessToken);

                    return new OkResult();
                    //return new OkObjectResult(result);
                }
                catch (Exception ex)
                {
                    return new BadRequestResult();
                }
            }

            return new UnauthorizedResult();
        }
    }
}
