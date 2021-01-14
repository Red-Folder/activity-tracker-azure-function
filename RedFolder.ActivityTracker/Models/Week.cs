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
            var start = ISOWeek.ToDateTime(year, weekNumber, DayOfWeek.Monday);
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
            var year = ISOWeek.GetYear(date);
            var weekNumber = ISOWeek.GetWeekOfYear(date);

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
    }
}
