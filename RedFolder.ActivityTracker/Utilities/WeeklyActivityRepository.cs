using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;
using RedFolder.ActivityTracker.Services;
using System;
using System.Threading.Tasks;

namespace RedFolder.ActivityTracker.Utilities
{
    public class WeeklyActivityRepository
    {
        private readonly Models.Week _week;
        private readonly Binder _binder;
        private ILogger _log;

        public WeeklyActivityRepository(Models.Week week, Binder binder, ILogger log)
        {
            _week = week;
            _binder = binder;
            _log = log;
        }

        public async Task Update(Action<Models.WeekActivity> action)
        {
            var blobName = $"activity-weekly/{_week.Year.ToString("0000")}/{_week.WeekNumber.ToString("00")}.json";

            _log.LogInformation($"To save to {blobName}");

            var attributes = new Attribute[]
            {
                new BlobAttribute(blobName),
                new StorageAccountAttribute("AzureWebJobsStorage")
            };

            var blob = await _binder.BindAsync<CloudBlockBlob>(attributes);

            using (var locker = new CloudBlockBlobLocker<Models.WeekActivity>(blob))
            {
                var activity = locker.IsNew ?
                                new Models.WeekActivity(_week.Year, _week.WeekNumber) :
                                await locker.Download();

                action(activity);

                await locker.Upload(activity);
            }
        }
    }
}
