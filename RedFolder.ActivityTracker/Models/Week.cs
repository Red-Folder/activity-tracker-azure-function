using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace RedFolder.ActivityTracker.Models
{
    public class Week
    {
        public int Year { get; set; }
        public int WeekNumber { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public static Week FromYearAndWeekNumber(int year, int weekNumber)
        {
            var start = FirstDateOfWeekISO8601(year, weekNumber);
            var end = start.AddDays(7).AddMilliseconds(-1);
            return new Week
            {
                Year = year,
                WeekNumber = weekNumber,
                Start = start,
                End = end
            };
        }

        public static Week FromDate(DateTime date)
        {
            CultureInfo culture = CultureInfo.CurrentCulture;
            int weekNumber = culture.Calendar.GetWeekOfYear(
                                date,
                                CalendarWeekRule.FirstDay,
                                DayOfWeek.Monday);

            int year = weekNumber == 52 && date.Month == 1 ? date.Year - 1 : date.Year;

            return FromYearAndWeekNumber(year, weekNumber);
        }

        public static Week Current()
        {
            return FromDate(DateTime.Now);
        }

        public static Week LastWeek()
        {
            return FromDate(DateTime.Now.AddDays(-7));
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
