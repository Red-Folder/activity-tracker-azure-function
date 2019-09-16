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

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<Model>(requestBody);

            if (data.Year == default(int)) return new BadRequestObjectResult("Please provide year");
            if (data.WeekNumber == default(int)) return new BadRequestObjectResult("Please provide year");

            if (string.IsNullOrEmpty(data.Title)) return new BadRequestObjectResult("Please provide book title");
            if (string.IsNullOrEmpty(data.Description)) return new BadRequestObjectResult("Please provide book description");
            if (string.IsNullOrEmpty(data.Author)) return new BadRequestObjectResult("Please provide book author");
            if (string.IsNullOrEmpty(data.ImageUrl)) return new BadRequestObjectResult("Please provide book image url");

            var book = new Models.Book();
            book.Title = data.Title;
            book.Description = data.Description;
            book.Author = data.Author;
            book.ImageUrl = data.ImageUrl;

            var week = Models.Week.FromYearAndWeekNumber(data.Year, data.WeekNumber);

            var repository = new WeeklyActivityRepository(week, binder, log);

            await repository.Update(x => x.AddBook(book));

            return new StatusCodeResult(201);
        }

        public class Model
        {
            public int Year { get; set; }
            public int WeekNumber { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Author { get; set; }
            public string ImageUrl { get; set; }
        }
    }
}
