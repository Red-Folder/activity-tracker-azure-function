#r "Microsoft.WindowsAzure.Storage"
#r "Newtonsoft.Json"

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using Microsoft.Azure.WebJobs.Host.Bindings.Runtime;
using System.Globalization;
using Newtonsoft.Json;

public static async Task Run(PodCast podCast, Binder binder, TraceWriter log)
{
    CultureInfo culture = CultureInfo.CurrentCulture;    
  
    var dated = podCast.Created;
    int weekNumber = culture.Calendar.GetWeekOfYear(    
                        dated,    
                        CalendarWeekRule.FirstDay,    
                        DayOfWeek.Monday);    
  
    int year = weekNumber == 52 && dated.Month == 1 ? dated.Year - 1 : dated.Year;  

    var blobName = $"activity-weekly/{year.ToString("0000")}/{weekNumber.ToString("00")}.json";

    log.Info($"To save to {blobName}");

    var attributes = new Attribute[]
    {    
        new BlobAttribute(blobName),
        new StorageAccountAttribute("AzureWebJobsStorage")
    };

    var blob = await binder.BindAsync<CloudBlockBlob>(attributes);
    if (!blob.Exists())
    {
        log.Info("Blob not exist - create temporary");
        await blob.UploadTextAsync("");
    }
    string lease = blob.AcquireLease(TimeSpan.FromSeconds(15), null);
    var accessCondition = AccessCondition.GenerateLeaseCondition(lease);

    var json = await blob.DownloadTextAsync();
    var week = json.Length == 0 ? 
                    new WeekActivity(year, weekNumber) :
                    JsonConvert.DeserializeObject<WeekActivity>(json);

    week.AddPodCast(podCast);

    await blob.UploadTextAsync(JsonConvert.SerializeObject(week), null, accessCondition, null, null);
    blob.ReleaseLease(accessCondition);
}

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

public class PodCastActivity
{
    [JsonProperty("categories")]
    public List<PodCastCategory> Categories;

    [JsonIgnore]
    public long TotalDuration
    {
        get
        {
            if (Categories == null || Categories.Count() == 0) return 0;

            return Categories.Aggregate((long)0, (acc, x) => acc + x.TotalDuration);
        }
    }

    public void Add(PodCast podCast)
    {
        if (Categories == null) Categories = new List<PodCastCategory>();

        var categoryName = PodCastCategory(podCast);
        var category = Categories.Where(x => x.Name == categoryName).FirstOrDefault();
        if (category == null) 
        {
            category = new PodCastCategory(categoryName);
            Categories.Add(category);
        }

        category.AddPodCast(podCast);
    }

    private String PodCastCategory(PodCast podCast)
    {
        switch (podCast.FeedName) 
        {
            case "SANS Internet Storm Center Daily Network Cyber Security and Information Security Podcast":
            case "Risky Business":
            case "Troy Hunt's Weekly Update Podcast":
                return "Security";

            case "The InfoQ Podcast":
            case "Weekly Dev Tips":
            case "Hanselminutes":
            case "Software Engineering Radio - The Podcast for Professional Software Developers":
            case "Legacy Code Rocks":
                return "General Development";

            case "JavaScript Jabber":
                return "JavaScript";

            case "Adventures in Angular":
                return "Angular";

            case "NET Rocks":
                return ".Net & C#";

            case "React Round Up":
            case "The React Podcast":
                return "React & Redux";

            case "PodCTL - Containers | Kubernetes | OpenShift":
                return "Containers";

            case "The Azure Podcast":
                return "Azure";

            case "More or Less: Behind the Stats":
            case "Friday Night Comedy from BBC Radio 4":
                return "Fun";            

            case "Engineering Culture by InfoQ":
                return "Leadership";

            case "RunAs Radio":
            case "The Freelancers'Show":
            default:
                return "Other";
        }
    }
}

public class PodCastCategory
{
    public PodCastCategory()
    {

    }

    public PodCastCategory(String name)
    {
        Name = name;
    }

    [JsonProperty("name")]
    public String Name { get; set; }

    [JsonProperty("podCasts")]
    public List<PodCast> PodCasts { get; set; }

    [JsonIgnore]
    public long TotalDuration
    {
        get
        {
            if (PodCasts == null || PodCasts.Count() == 0) return 0;
            return PodCasts.Aggregate((long)0, (acc, x) => acc + x.EpisodeDuration);
        }
    }

    public void AddPodCast(PodCast podCast)
    {
        if (PodCasts == null) PodCasts = new List<PodCast>();
        PodCasts.Add(podCast);
    }
}

public class PodCast
{
    [JsonProperty("created")]
    public DateTime Created { get; set; }

    [JsonProperty("playing")]
    public bool Playing { get; set; }

    [JsonProperty("feedName")]
    public string FeedName { get; set; }

    [JsonProperty("feedUrl")]
    public string FeedUrl { get; set; }

    [JsonProperty("episodeName")]
    public string EpisodeName { get; set; }

    [JsonProperty("episodeUrl")]
    public string EpisodeUrl { get; set; }

    [JsonProperty("episodeFile")]
    public string EpisodeFile { get; set; }

    [JsonProperty("episodePostUrl")]
    public string EpisodePostUrl { get; set; }

    [JsonProperty("episodeMime")]
    public string EpisodeMime { get; set; }

    [JsonProperty("episodeSummary")]
    public string EpisodeSummary { get; set; }

    [JsonProperty("episodeDuration")]
    public long EpisodeDuration { get; set; }

    [JsonProperty("episodePosition")]
    public long EpisodePosition { get; set; }

    [JsonProperty("artist")]
    public string Artist { get; set; }

    [JsonProperty("album")]
    public string Album { get; set; }

    [JsonProperty("track")]
    public string Track { get; set; }

    [JsonProperty("actioned")]
    public bool Actioned { get; set; }
}