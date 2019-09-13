using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RedFolder.ActivityTracker.Services;
using System;
using Microsoft.WindowsAzure.Storage.Blob;

namespace RedFolder.ActivityTracker.Books
{
    public static class AddBook
    {
        [FunctionName("AddBook")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            Binder binder,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            var book = new Models.Book();
            book.Title = data?.title;
            book.Description = data?.description;
            book.Author = data?.author;
            book.ImageUrl = data?.imageUrl;

            if (string.IsNullOrEmpty(book.Title)) return new BadRequestObjectResult("Please provide book title");
            if (string.IsNullOrEmpty(book.Description)) return new BadRequestObjectResult("Please provide book description");
            if (string.IsNullOrEmpty(book.Author)) return new BadRequestObjectResult("Please provide book author");
            if (string.IsNullOrEmpty(book.ImageUrl)) return new BadRequestObjectResult("Please provide book image url");

            int year;
            int weekNumber;

            if (!int.TryParse(data?.year, out year)) return new BadRequestObjectResult("Please provide valid year");
            if (!int.TryParse(data?.weekNumber, out weekNumber)) return new BadRequestObjectResult("Please provide valid weekNumber");

            var week = Models.Week.FromYearAndWeekNumber(year, weekNumber);

            var blobName = $"activity-weekly/{week.Year.ToString("0000")}/{week.WeekNumber.ToString("00")}.json";

            log.LogInformation($"To save to {blobName}");

            var attributes = new Attribute[]
            {
                new BlobAttribute(blobName),
                new StorageAccountAttribute("AzureWebJobsStorage")
            };

            var blob = await binder.BindAsync<CloudBlockBlob>(attributes);

            using (var locker = new CloudBlockBlobLocker<Models.WeekActivity>(blob))
            {
                var activity = locker.IsNew ?
                                new Models.WeekActivity(week.Year, week.WeekNumber) :
                                await locker.Download();

                activity.AddBook(book);

                await locker.Upload(activity);
            }

            return new StatusCodeResult(201);
        }
    }
}
