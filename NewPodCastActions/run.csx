using System;

public static void Run(PodCast newPodCast, ICollector<PodCast> toBeTweeted, ICollector<PodCast> toBeAddedToWeeklyActivity, TraceWriter log)
{
    var podCastAge = (DateTime.Now - newPodCast.Created).TotalHours;

    if (podCastAge < 3)
    {
        toBeTweeted.Add(newPodCast);
    }

    toBeAddedToWeeklyActivity.Add(newPodCast);
}

public class PodCast
{
    public DateTime Created { get; set; }
    public bool Playing { get; set; }
    public string FeedName { get; set; }
    public string FeedUrl { get; set; }
    public string EpisodeName { get; set; }
    public string EpisodeUrl { get; set; }
    public string EpisodeFile { get; set; }
    public string EpisodePostUrl { get; set; }
    public string EpisodeMime { get; set; }
    public string EpisodeSummary { get; set; }
    public long EpisodeDuration { get; set; }
    public long EpisodePosition { get; set; }
    public string Artist { get; set; }
    public string Album { get; set; }
    public string Track { get; set; }

    public bool Actioned { get; set; }
}