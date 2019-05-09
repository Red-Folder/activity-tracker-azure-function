using Microsoft.AspNetCore.Http;
using System;

namespace RedFolder.ActivityTracker.Utilities
{
    public class Authentication
    {
        private const string X_MS_CLIENT_PRINCIPAL_ID = "X-MS-CLIENT-PRINCIPAL-ID";
        private const string X_MS_CLIENT_PRINCIPAL_NAME = "X-MS-CLIENT-PRINCIPAL-NAME";
        private const string X_MS_TOKEN_AAD_ID_TOKEN = "X-MS-TOKEN-AAD-ID-TOKEN";

        public Authentication(HttpRequest httpRequest)
        {
            if (httpRequest == null || httpRequest.Headers == null) return;

            if (!string.IsNullOrEmpty(httpRequest.Headers[X_MS_CLIENT_PRINCIPAL_ID])) UserId = httpRequest.Headers[X_MS_CLIENT_PRINCIPAL_ID];
            if (!string.IsNullOrEmpty(httpRequest.Headers[X_MS_CLIENT_PRINCIPAL_NAME])) UserName = httpRequest.Headers[X_MS_CLIENT_PRINCIPAL_NAME];
            if (!string.IsNullOrEmpty(httpRequest.Headers[X_MS_TOKEN_AAD_ID_TOKEN])) Token = httpRequest.Headers[X_MS_TOKEN_AAD_ID_TOKEN];
        }

        public bool IsAuthorised => !string.IsNullOrEmpty(UserId);

        public string UserId { get; }
        public string UserName { get; }
        public string Token { get; }

        public bool Disabled
        {
            get
            {
                try
                {
                    var authenticationConfig = Environment.GetEnvironmentVariable("Authentication", EnvironmentVariableTarget.Process);

                    if (authenticationConfig == "DISABLED") return true;
                }
                catch (Exception ex)
                {
                }

                return false;
            }
        }
    }
}
