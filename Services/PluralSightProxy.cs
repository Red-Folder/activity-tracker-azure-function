using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Red_Folder.ActivityTracker.Models;
using Red_Folder.ActivityTracker.Models.Pluralsight;
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
        private IList<CurrentlyLearningCourse> _currentlyLearning;
        private IList<CompletedCourse> _completed;

        public PluralsightProxy(ILogger log)
        {
            _log = log;
        }

        public async Task PopulateAsync(string pluralSightId, DateTime start, DateTime end)
        {
            _currentlyLearning = await GetActivityDataAsync<CurrentlyLearningCourse>(pluralSightId, start, end);
        }

        public PluralsightActivity GetPluralsightActivity()
        {
            var result = new PluralsightActivity();

            result.Courses = _currentlyLearning.Select(raw => new Models.Course
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

        private async Task<IList<T>> GetActivityDataAsync<T>(string pluralsightId, DateTime start, DateTime end) where T : BaseCourse
        {
            using (var client = new HttpClient())
            {
                var url = BuildUrl(pluralsightId);

                var courses = await GetAsync<T>(client, url, start, end);

                return courses;
            }
        }

        private async Task<IList<T>> GetAsync<T>(HttpClient client, string url, DateTime start, DateTime end) where T: BaseCourse
        {
            var courses = new List<T>();

            _log.LogInformation($"Retrieving data from {url}");
            var result = await client.GetAsync($"{url}");

            if (result.IsSuccessStatusCode)
            {
                string json = await result.Content.ReadAsStringAsync();
                _log.LogInformation($"Received Json: {json}");
                var fullResponse = JsonConvert.DeserializeObject<List<T>>(json);

                _log.LogInformation($"{fullResponse.Count()} records in fullResponse");

                if (typeof(T) == typeof(CurrentlyLearningCourse))
                {
                    courses = fullResponse.Where(course => ((CurrentlyLearningCourse)course).LastViewedTimestamp >= start && course.LastViewedTimestamp <= end).ToList();
                }

                _log.LogInformation($"{courses.Count()} records in courses");
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
