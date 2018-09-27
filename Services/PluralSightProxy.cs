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
        private IList<Contents> _courseContent = new List<Contents>();

        public PluralsightProxy(ILogger log)
        {
            _log = log;
        }

        public async Task PopulateAsync(string pluralSightId, DateTime start, DateTime end)
        {
            _currentlyLearning = await GetActivityDataAsync<CurrentlyLearningCourse>("currentlylearning", pluralSightId, start, end);
            _completed = await GetActivityDataAsync<CompletedCourse>("completedcourses", pluralSightId, start, end);

        }

        private async Task PopulateCourseContents<T>(HttpClient client, IList<T> courses) where T : BaseCourse
        {
            foreach (var course in courses)
            {
                if (!_courseContent.Any(x => x.Id == course.CourseId))
                {
                    try
                    {
                        _courseContent.Add(await GetCourseContents(client, course.CourseId));
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        public PluralsightActivity GetPluralsightActivity()
        {
            var result = new PluralsightActivity();

            var combinedCourses = new List<Course>();
            combinedCourses.AddRange(_completed.Select(raw =>
            {
                var content = _courseContent.Where(x => x.Id == raw.CourseId).FirstOrDefault();
                return new Course
                {
                    CourseId = raw.CourseId,
                    Title = raw.Title,
                    Url = $"https://www.pluralsight.com/courses/{raw.CourseId}",
                    PercentageComplete = 100,
                    CourseImageUrl = content == null ? null : content.CourseImageUrl,
                    ShortDescription = content == null ? "" : content.ShortDescription,
                    Description = content == null ? "" : content.Description
                };
            }).ToList());
            combinedCourses.AddRange(_currentlyLearning.Select(raw =>
            {
                var content = _courseContent.Where(x => x.Id == raw.CourseId).FirstOrDefault();
                return new Course
                {
                    CourseId = raw.CourseId,
                    Title = raw.Title,
                    Url = $"https://www.pluralsight.com/courses/{raw.CourseId}",
                    PercentageComplete = (int)raw.PercentageComplete,
                    CourseImageUrl = content == null ? null : content.CourseImageUrl,
                    ShortDescription = content == null ? "" : content.ShortDescription,
                    Description = content == null ? "" : content.Description
                };
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

                await PopulateCourseContents<T>(client, courses);

                return courses;
            }
        }

        private async Task<IList<T>> GetAsync<T>(HttpClient client, string url, DateTime start, DateTime end) where T : BaseCourse
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

        private async Task<Contents> GetCourseContents(HttpClient client, string id)
        {
            var url = $"https://app.pluralsight.com/learner/content/courses/{id}";
            _log.LogInformation($"Retrieving data from {url}");
            var result = await client.GetAsync($"{url}");

            if (result.IsSuccessStatusCode)
            {
                string json = await result.Content.ReadAsStringAsync();
                var contents = JsonConvert.DeserializeObject<Contents>(json);

                return contents;
            }
            else
            {
                _log.LogError($"Failed to retrieve data, status code: {result.StatusCode}");
                throw new ApplicationException("Error occurred while trying to retrieve data");
            }
        }
    }
}
