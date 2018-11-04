using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Red_Folder.ActivityTracker.Services
{
    public class BlogProxy
    {
        private ILogger _log;

        public BlogProxy(ILogger log)
        {
            _log = log;
        }

        public async Task<Models.BlogActivity> GetBlogActivityAsync(string url, DateTime start, DateTime end)
        {
            return new Models.BlogActivity
            {
                Blogs = await GetAsync(url, start, end)
            };
        }

        private async Task<List<Models.Blog>> GetAsync(string url, DateTime start, DateTime end)
        {
            var blogs = new List<Models.Blog>();

            using (var client = new HttpClient())
            {
                _log.LogInformation($"Retrieving data from {url}");
                var result = await client.GetAsync($"{url}");

                if (result.IsSuccessStatusCode)
                {
                    string json = await result.Content.ReadAsStringAsync();
                    var rawBlogs = JsonConvert.DeserializeObject<List<Models.BlogService.Blog>>(json);
                    _log.LogInformation($"{rawBlogs.Count()} blogs found");

                    blogs = rawBlogs
                                .Where(x => x.Enabled)
                                .Where(x => x.Published >= start && x.Published <= end)
                                .Select(x => new Models.Blog
                                {
                                    Id = x.Id,
                                    Url = x.Url,
                                    Published = x.Published,
                                    Title = x.Title,
                                    Image = x.Image,
                                    Description = x.Description,
                                    KeyWords = x.KeyWords
                                })
                                .ToList();

                    _log.LogInformation($"{blogs.Count()} blogs in the date range");
                }
                else
                {
                    _log.LogError($"Failed to retrieve data, status code: {result.StatusCode}");
                    throw new ApplicationException("Error occurred while trying to retrieve data");
                }

                return blogs;
            }
        }
    }
}
