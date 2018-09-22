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
            _currentlyLearning = await GetActivityDataAsync<CurrentlyLearningCourse>("currentlylearning", pluralSightId, start, end);
            _completed = await GetActivityDataAsync<CompletedCourse>("completedcourses", pluralSightId, start, end);
        }

        public PluralsightActivity GetPluralsightActivity()
        {
            var result = new PluralsightActivity();

            var combinedCourses = new List<Course>();
            combinedCourses.AddRange(_completed.Select(raw => new Course
            {
                Name = raw.Title,
                Url = $"https://www.pluralsight.com/courses/{raw.CourseId}",
                PercentageComplete = 100
            }).ToList());
            combinedCourses.AddRange(_currentlyLearning.Select(raw => new Course
            {
                Name = raw.Title,
                Url = $"https://www.pluralsight.com/courses/{raw.CourseId}",
                PercentageComplete = (int)raw.PercentageComplete
            }).ToList());

            return new PluralsightActivity
            {
                Courses = combinedCourses
            };
        }

        private async Task<IList<T>> GetActivityDataAsync<T>(string apiMethod, string pluralsightId, DateTime start, DateTime end) where T : BaseCourse
        {
            using (var client = new HttpClient())
            {
                var url = $"https://app.pluralsight.com/profile/data/{apiMethod}/{pluralsightId}";

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
                var fullResponse = JsonConvert.DeserializeObject<List<T>>(json);
                _log.LogInformation($"{fullResponse.Count()} records in fullResponse");

                courses = fullResponse.Where(course => course.IsWithinRange(start, end)).ToList();
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
