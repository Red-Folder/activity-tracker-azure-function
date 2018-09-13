using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Red_Folder.ActivityTracker.Models
{
    public class PodCastTableEntity : TableEntity
    {
        public PodCastTableEntity() : base()
        {
        }

        public PodCastTableEntity(string partitionKey, string rowKey) : base(partitionKey, rowKey)
        {
        }

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

        public PodCast ToPodCast()
        {
            return new PodCast
            {
                Created = this.Created,
                Playing = this.Playing,
                FeedName = this.FeedName,
                FeedUrl = this.FeedUrl,
                EpisodeName = this.EpisodeName,
                EpisodeUrl = this.EpisodeUrl,
                EpisodeFile = this.EpisodeFile,
                EpisodePostUrl = this.EpisodePostUrl,
                EpisodeMime = this.EpisodeMime,
                EpisodeSummary = this.EpisodeSummary,
                EpisodeDuration = this.EpisodeDuration,
                EpisodePosition = this.EpisodePosition,
                Artist = this.Artist,
                Album = this.Album,
                Track = this.Track
            };
        }
    }
}
