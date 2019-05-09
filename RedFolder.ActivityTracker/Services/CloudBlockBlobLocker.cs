using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RedFolder.ActivityTracker.Services
{
    public class CloudBlockBlobLocker<T> : IDisposable
    {
        private CloudBlockBlob _blob;
        private AccessCondition _accessCondition;
        private bool _isNew = false;

        public CloudBlockBlobLocker(CloudBlockBlob blob)
        {
            var exists = blob.ExistsAsync().Result;
            if (!exists)
            {
                blob.UploadTextAsync("{}").Wait();
                _isNew = true;
            }

            var lease = blob.AcquireLeaseAsync(TimeSpan.FromSeconds(15), null).Result;
            _accessCondition = AccessCondition.GenerateLeaseCondition(lease);

            _blob = blob;    
        }

        public bool IsNew
        {
            get
            {
                return _isNew;
            }
        }

        public async Task<T> Download()
        {
            if (_isNew) throw new ApplicationException("IsNew - cannot download");

            var json = await _blob.DownloadTextAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task Upload(T model)
        {
            await _blob.UploadTextAsync(JsonConvert.SerializeObject(model), null, _accessCondition, null, null);
        }

        public void Dispose()
        {
            _blob.ReleaseLeaseAsync(_accessCondition).Wait();
        }
    }
}
