using Microsoft.WindowsAzure.Storage.Table;
using RedFolder.ActivityTracker.Models.BeyondPod;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedFolder.ActivityTracker.IntegrationTests.Utils
{
    public static class CloudTableHelper
    {
        public static async Task<List<PodCastTableEntity>> GetAllCloudTableEntities(CloudTable cloudTable)
        {
            var get = await cloudTable.ExecuteQuerySegmentedAsync(new TableQuery<PodCastTableEntity>(), new TableContinuationToken());

            return get.Results;
        }

        public static async Task AddToCloudTableEntities(CloudTable cloudTable, PodCastTableEntity entity)
        {
            await cloudTable.ExecuteAsync(TableOperation.Insert(entity));
        }

        public static Models.BeyondPod.PodCast BeyondPodPodCast = new Models.BeyondPod.PodCast
        {
            Created = DateTime.Now,
            Playing = true,
            FeedName = "NET Rocks",
            FeedUrl = "http://feeds.feedburner.com/netRocksFullMp3Downloads",
            EpisodeName = ".NET Core 3 and Beyond with Scott Hunter",
            EpisodeUrl = "http://feedproxy.google.com/~r/netRocksFullMp3Downloads/~5/csNN236Wlys/e36199af23789d048885158bc55257d7.mp3",
            EpisodeFile = "/storage/emulated/0/Android/data/mobi.beyondpod/files/BeyondPod/Podcasts/NET Rocks/NET Rocks-2031992760.mp3",
            EpisodePostUrl = "http://feedproxy.google.com/~r/netRocksFullMp3Downloads/~3/A90Z67z1414/default.aspx",
            EpisodeMime = "audio/mpeg",
            EpisodeSummary = "Build is over - what did we learn? Carl and Richard talk to Scott Hunter about the various announcements at Build connection with .NET - including the delivery date of .NET Core 3 and what happens beyond! The conversation digs into switching to a routine delivery model for .NET, so that you can anticipate when you'll need to implement the new version of the framework. Scott also talks about new features coming in C# 8, including the fact that C# 8 is only for .NET Core 3 and above... things are",
            EpisodeDuration = 3042,
            EpisodePosition = 100,
            Artist = "NET Rocks",
            Album = "NET Rocks",
            Track = ".NET Core 3 and Beyond with Scott Hunter"
        };

        public static PodCastTableEntity TableEntityPodCast = new PodCastTableEntity
        {
            PartitionKey = "NET Rocks",
            RowKey = ".NET Core 3 and Beyond with Scott Hunter",
            Created = DateTime.Now,
            Playing = true,
            FeedName = "NET Rocks",
            FeedUrl = "http://feeds.feedburner.com/netRocksFullMp3Downloads",
            EpisodeName = ".NET Core 3 and Beyond with Scott Hunter",
            EpisodeUrl = "http://feedproxy.google.com/~r/netRocksFullMp3Downloads/~5/csNN236Wlys/e36199af23789d048885158bc55257d7.mp3",
            EpisodeFile = "/storage/emulated/0/Android/data/mobi.beyondpod/files/BeyondPod/Podcasts/NET Rocks/NET Rocks-2031992760.mp3",
            EpisodePostUrl = "http://feedproxy.google.com/~r/netRocksFullMp3Downloads/~3/A90Z67z1414/default.aspx",
            EpisodeMime = "audio/mpeg",
            EpisodeSummary = "Build is over - what did we learn? Carl and Richard talk to Scott Hunter about the various announcements at Build connection with .NET - including the delivery date of .NET Core 3 and what happens beyond! The conversation digs into switching to a routine delivery model for .NET, so that you can anticipate when you'll need to implement the new version of the framework. Scott also talks about new features coming in C# 8, including the fact that C# 8 is only for .NET Core 3 and above... things are",
            EpisodeDuration = 3042,
            EpisodePosition = 100,
            Artist = "NET Rocks",
            Album = "NET Rocks",
            Track = ".NET Core 3 and Beyond with Scott Hunter"
        };
    }
}
