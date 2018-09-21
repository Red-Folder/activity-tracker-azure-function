using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Red_Folder.ActivityTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Red_Folder.ActivityTracker.Services
{
    public class PluralsightProxy
    {
        private ILogger _log;
        private IList<Models.Pluralsight.Course> _courses;

        public PluralsightProxy(ILogger log)
        {
            _log = log;
        }

        public async Task PopulateAsync(string pluralSightId, DateTime start, DateTime end)
        {
            _courses = await GetActivityDataAsync(pluralSightId, start, end);
        }

        public PluralsightActivity GetPluralsightActivity()
        {
            var result = new PluralsightActivity();

            result.Courses = _courses.Select(raw => new Models.Course
            {
                Name = raw.Title,
                Url = $"https://www.pluralsight.com/courses/{raw.CourseId}",
                PercentageComplete = (int)raw.PercentageComplete
            }).ToList();

            return result;
        }

        private string BuildUrl(string pluralsightId)
        {
            var builder = new StringBuilder();
            builder.Append("https://app.pluralsight.com/profile/data/currentlylearning/");
            builder.Append(pluralsightId);

            return builder.ToString();
        }

        private async Task<IList<Models.Pluralsight.Course>> GetActivityDataAsync(string pluralsightId, DateTime start, DateTime end)
        {
            using (var client = new HttpClient())
            {
                var url = BuildUrl(pluralsightId);

                var courses = await GetAsync(client, url, start, end);

                return courses;
            }
        }

        private async Task<IList<Models.Pluralsight.Course>> GetAsync(HttpClient client, string url, DateTime start, DateTime end)
        {
            var courses = new List<Models.Pluralsight.Course>();

            _log.LogInformation($"Retrieving data");
            var result = await client.GetAsync($"{url}");

            if (result.IsSuccessStatusCode)
            {
                string json = await result.Content.ReadAsStringAsync();
                var fullResponse = JsonConvert.DeserializeObject<Models.Pluralsight.Course[]>(json);

                courses = fullResponse.Where(course => course.LastViewedTimestamp >= start && course.LastViewedTimestamp <= end).ToList();
            }
            else
            {
                _log.LogError($"Failed to retrieve data, status code: {result.StatusCode}");
                throw new ApplicationException("Error occurred while trying to retrieve data");
            }

            return courses;
        }

    }
}
