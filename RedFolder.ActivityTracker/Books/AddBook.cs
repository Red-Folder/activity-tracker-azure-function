using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RedFolder.ActivityTracker.Utilities;

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

            if (data?.year as int? == null) return new BadRequestObjectResult("Please provide year");
            if (data?.weekNumber as int? == null) return new BadRequestObjectResult("Please provide weekNumber");

            int year = data?.year ?? default(int);
            int weekNumber = data?.weekNumber ?? default(int);

            var week = Models.Week.FromYearAndWeekNumber(year, weekNumber);

            var repository = new WeeklyActivityRepository(week, binder, log);

            await repository.Update(x => x.AddBook(book));

            return new StatusCodeResult(201);
        }
    }
}
