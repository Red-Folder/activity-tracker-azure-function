using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RedFolder.ActivityTracker.Utilities;
using System.IO;
using System.Threading.Tasks;

namespace RedFolder.ActivityTracker.Events
{
    public class AddEvent
    {
        [FunctionName("AddEvent")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            Binder binder,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<Model>(requestBody);

            if (data.Year == default(int)) return new BadRequestObjectResult("Please provide year");
            if (data.WeekNumber == default(int)) return new BadRequestObjectResult("Please provide year");

            if (string.IsNullOrEmpty(data.Title)) return new BadRequestObjectResult("Please provide event title");
            if (string.IsNullOrEmpty(data.Description)) return new BadRequestObjectResult("Please provide event description");
            if (string.IsNullOrEmpty(data.ImageUrl)) return new BadRequestObjectResult("Please provide event image url");

            var newEvent = new Models.Event();
            newEvent.Title = data.Title;
            newEvent.Description = data.Description;
            newEvent.ImageUrl = data.ImageUrl;

            var week = Models.Week.FromYearAndWeekNumber(data.Year, data.WeekNumber);

            var repository = new WeeklyActivityRepository(week, binder, log);

            await repository.Update(x => x.AddEvent(newEvent));

            return new StatusCodeResult(201);
        }

        public class Model
        {
            public int Year { get; set; }
            public int WeekNumber { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string ImageUrl { get; set; }
        }
    }
}
