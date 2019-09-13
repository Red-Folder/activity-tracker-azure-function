using Newtonsoft.Json;
using System;
using System.Globalization;

namespace RedFolder.ActivityTracker.Models
{
    public class WeekActivity
    {
        private Week _week;

        public WeekActivity()
        {
            _week = new Week();
        }

        public WeekActivity(int year, int weekNumber)
        {
            _week = Week.FromYearAndWeekNumber(year, weekNumber);
        }

        [JsonProperty("year")]
        public int Year
        {
            get => _week.Year;
            set => _week.Year = value;
        }

        [JsonProperty("weekNumber")]
        public int WeekNumber
        {
            get => _week.WeekNumber;
            set => _week.WeekNumber = value;
        }

        [JsonProperty("start")]
        public DateTime Start
        {
            get => _week.Start;
            set => _week.Start = value;
        }

        [JsonProperty("end")]
        public DateTime End
        {
            get => _week.End;
            set => _week.End = value;
        }

        [JsonProperty("podCasts")]
        public PodCastActivity PodCasts { get; set; }

        [JsonProperty("skills")]
        public SkillsActivity Skills { get; set; }

        [JsonProperty("focus")]
        public FocusActivity Focus { get; set; }

        [JsonProperty("pluralsight")]
        public PluralsightActivity Pluralsight { get; set; }

        [JsonProperty("clients")]
        public ClientActivity Clients { get; set; }

        [JsonProperty("blogs")]
        public BlogActivity Blogs { get; set; }

        [JsonProperty("events")]
        public EventActivity Events { get; set; }

        [JsonProperty("books")]
        public BookActivity Books { get; set; }

        public void AddPodCast(PodCast podCast)
        {
            if (PodCasts == null) PodCasts = new PodCastActivity();

            PodCasts.Add(podCast);
        }

        public void AddBook(Book book)
        {
            if (Books == null) Books = new BookActivity();

            Books.Add(book);
        }
    }
}
