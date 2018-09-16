using Newtonsoft.Json;
using System;
using System.Globalization;

namespace Red_Folder.ActivityTracker.Models
{
    public class WeekActivity
    {
        public WeekActivity()
        {

        }

        public WeekActivity(int year, int weekNumber)
        {
            Year = year;
            WeekNumber = weekNumber;

            Start = FirstDateOfWeekISO8601(Year, WeekNumber);
            End = Start.AddDays(7).AddMilliseconds(-1);
        }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("weekNumber")]
        public int WeekNumber { get; set; }

        [JsonProperty("start")]
        public DateTime Start { get; set; }

        [JsonProperty("end")]
        public DateTime End { get; set; }

        [JsonProperty("podCasts")]
        public PodCastActivity PodCasts { get; set; }

        [JsonProperty("skills")]
        public SkillsActivity Skills { get; set; }


        public void AddPodCast(PodCast podCast)
        {
            if (PodCasts == null) PodCasts = new PodCastActivity();

            PodCasts.Add(podCast);
        }

        private static DateTime FirstDateOfWeekISO8601(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            // Use first Thursday in January to get first week of the year as
            // it will never be in Week 52/53
            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            // As we're adding days to a date in Week 1,
            // we need to subtract 1 in order to get the right date for week #1
            if (firstWeek == 1)
            {
                weekNum -= 1;
            }

            // Using the first Thursday as starting week ensures that we are starting in the right year
            // then we add number of weeks multiplied with days
            var result = firstThursday.AddDays(weekNum * 7);

            // Subtract 3 days from Thursday to get Monday, which is the first weekday in ISO8601
            return result.AddDays(-3);
        }

    }
}
