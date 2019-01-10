using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Red_Folder.ActivityTracker.Models.LinkedIn;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Red_Folder.ActivityTracker.Services
{
    public class LinkedInProxy
    {
        private string _clientId;
        private string _clientSecret;
        private ILogger _log;

        public LinkedInProxy(string clientId, string clientSecret, ILogger log)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _log = log;
        }

        public async Task<AccessTokenResponse> RequestAccessToken(AccessTokenRequest request)
        {
            using (var client = new HttpClient())
            {
                var postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
                postData.Add(new KeyValuePair<string, string>("code", request.Code));
                postData.Add(new KeyValuePair<string, string>("redirect_uri", request.RedirectUri));
                postData.Add(new KeyValuePair<string, string>("client_id", _clientId));
                postData.Add(new KeyValuePair<string, string>("client_secret", _clientSecret));

                var content = new FormUrlEncodedContent(postData);

                var response = await client.PostAsync("https://www.linkedin.com/oauth/v2/accessToken", content);

                string json = await response.Content.ReadAsStringAsync();

                var raw = JsonConvert.DeserializeObject<Models.LinkedIn.Raw.AccessTokenResponse>(json);

                return new AccessTokenResponse
                {
                    AccessToken = raw.AccessToken,
                    SecondsTillExpires = raw.ExpiresIn
                };
            }
        }
    }
}
