using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RedFolder.ActivityTracker.Models.LinkedIn;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RedFolder.ActivityTracker.Services
{
    public class LinkedInProxy
    {
        private string _userId;
        private string _clientId;
        private string _clientSecret;
        private ILogger _log;

        public LinkedInProxy(string userId, string clientId, string clientSecret, ILogger log)
        {
            _userId = userId;
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

        public async Task CreateShare(string accessToken)
        {
            using (var client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Add("X-Restli-Protocol-Version", "2.0.0");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var shareRequest = new Models.LinkedIn.Raw.ShareRequest
                {
                    Owner = _userId
                };

                var response = await client.PostAsJsonAsync<Models.LinkedIn.Raw.ShareRequest>("https://api.linkedin.com/v2/shares", shareRequest);

                string json = await response.Content.ReadAsStringAsync();

                //var raw = JsonConvert.DeserializeObject<Models.LinkedIn.Raw.AccessTokenResponse>(json);
            }
        }
    }
}
