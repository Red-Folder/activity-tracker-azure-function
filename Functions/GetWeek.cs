using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Red_Folder.ActivityTracker.Models;
using System;

namespace Red_Folder.ActivityTracker.Functions
{
    public static class GetWeek
    {
        [FunctionName("GetWeek")]
        public static Week Run(ILogger log)
        {
            log.LogInformation("Getting the week to work on.");

            var overrideWeek = Environment.GetEnvironmentVariable("OverrideWeek", EnvironmentVariableTarget.Process);

            var week = Week.LastWeek();
            if (!String.IsNullOrEmpty(overrideWeek))
            {
                log.LogInformation($"Override set for week: {overrideWeek}");
                var weekNumber = int.Parse(overrideWeek.Split(':')[0]);
                var year = int.Parse(overrideWeek.Split(':')[1]);

                week = Week.FromYearAndWeekNumber(year, weekNumber);
            }

            return week;
        }
    }
}
