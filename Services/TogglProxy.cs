﻿using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Red_Folder.ActivityTracker.Models;
using Red_Folder.ActivityTracker.Models.Toggl;
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
                                TotalDuration = group.Sum(x => x.Duration)
                            });

            result.Skills = skills.ToList();

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

                var url = BuildUrl(togglWorkspaceId, new DateTime(2018, 09, 08), new DateTime(2018, 09, 15));

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
                if (detailedReport.Pages > pageNo)
                {
                    timeEntries.AddRange(await GetPageAsync(client, url, pageNo++));
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