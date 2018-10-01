using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Red_Folder.ActivityTracker.Models;
using Red_Folder.ActivityTracker.Models.Toggl;
using Red_Folder.ActivityTracker.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Red_Folder.ActivityTracker.Services
{
    public class TogglProxy
    {
        private ILogger _log;
        private IList<TimeEntry> _timeEntries;

        public TogglProxy(ILogger log)
        {
            _log = log;
        }

        public async Task PopulateAsync(string togglApiKey, string togglWorkspaceId, DateTime start, DateTime end)
        {
            _timeEntries = await GetAcivityDataAsync(togglApiKey, togglWorkspaceId, start, end);
        }

        public SkillsActivity GetSkillsActivity()
        {
            var result = new SkillsActivity();

            var skillDurations = _timeEntries.SelectMany(entry => entry.tags.Where(tag => tag.StartsWith("Skill -")),
                                                        (entry, tag) => new { tag, entry.Duration }).ToList();

            var skills = skillDurations.GroupBy(skill => skill.tag)
                            .Select(group => new Skill
                            {
                                Name = group.Key.Replace("Skill - ", ""),
                                TotalDuration = group.Sum(x => (x.Duration/ 1000))
                            });

            result.Skills = skills.ToList();

            return result;
        }

        public FocusActivity GetFocusActivity()
        {
            var result = new FocusActivity();

            var focusDurations = _timeEntries.SelectMany(entry => entry.tags.Where(tag => tag.StartsWith("Focus -")),
                                                        (entry, tag) => new { tag, entry.Duration }).ToList();

            var focus = focusDurations.GroupBy(skill => skill.tag)
                            .Select(group => new Skill
                            {
                                Name = group.Key.Replace("Focus - ", ""),
                                TotalDuration = group.Sum(x => (x.Duration / 1000))
                            });

            result.Focus = focus.ToList();

            return result;
        }

        public ClientActivity GetClientActivity()
        {
            var result = new ClientActivity();

            var obscurer = new ClientObscurer();
            var clients = _timeEntries
                                .Where(entity => !String.IsNullOrEmpty(entity.Client))
                                .Select(entity => new {
                                    Client = obscurer.Obscure(entity.Client),
                                    Duration = entity.Duration
                                })
                                .GroupBy(entry => entry.Client)
                                .Select(group => new Client
                                {
                                    Name = group.Key,
                                    TotalDuration = group.Sum(x => x.Duration / 1000)
                                });

            result.Clients = clients.ToList();
            return result;
        }

        private string BuildUrl(string workspaceId, DateTime from, DateTime to)
        {
            var builder = new StringBuilder();
            builder.Append("https://toggl.com/reports/api/v2/details?");
            builder.Append($"workspace_id={workspaceId}");
            builder.Append($"&since={from.ToString("yyyy-MM-dd")}");
            builder.Append($"&until={to.ToString("yyyy-MM-dd")}");
            builder.Append("&user_agent=red-folder.com");

            return builder.ToString();
        }

        private async Task<IList<TimeEntry>> GetAcivityDataAsync(string togglApiKey, string togglWorkspaceId, DateTime start, DateTime end)
        {
            using (var client = new HttpClient())
            {
                var credentials = Encoding.ASCII.GetBytes($"{togglApiKey}:api_token");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentials));

                var url = BuildUrl(togglWorkspaceId, start, end);

                var timeEntries = await GetPageAsync(client, url, 1);

                return timeEntries;
            }
        }

        private async Task<IList<TimeEntry>> GetPageAsync(HttpClient client, string url, int pageNo)
        {
            var timeEntries = new List<TimeEntry>();

            _log.LogInformation($"Retrieving page no {pageNo}");
            var result = await client.GetAsync($"{url}&page={pageNo}");

            if (result.IsSuccessStatusCode)
            {
                string json = await result.Content.ReadAsStringAsync();
                var detailedReport = JsonConvert.DeserializeObject<DetailedReport>(json);
                timeEntries.AddRange(detailedReport.TimeEntries);

                if ((pageNo * detailedReport.PerPage) < detailedReport.TotalCount)
                {
                    timeEntries.AddRange(await GetPageAsync(client, url, pageNo+1));
                }
            }
            else
            {
                _log.LogError($"Failed to retrieve data, status code: {result.StatusCode}");
                throw new ApplicationException("Error occurred while trying to retrieve data");
            }

            return timeEntries;
        }

    }
}
