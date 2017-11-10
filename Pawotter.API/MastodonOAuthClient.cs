using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Pawotter.API
{
    public class MastodonOAuthClient : BaseApiClient, IMastodonOAuthClient
    {
        public MastodonOAuthClient(Uri baseUri, HttpClient http) : base(baseUri, http) { }

        public async Task<Core.Entities.OAuth.Application> CreateApp(string clientName, Uri redirectUris, Core.Entities.OAuth.AccessScope scope, CancellationToken? token = null)
        {
            var data = new Dictionary<string, string> {
                { "client_name", clientName },
                { "redirect_uris", redirectUris.AbsoluteUri },
                { "scope", scope.QueryString }
            };
            var response = await PostAsync("/api/v1/apps", data, null, token);
            return await response
                .Content.ReadAsStringAsync()
                .ContinueWith((task) => JsonConvert.DeserializeObject<Core.Entities.OAuth.Application>(task.Result));
        }

        public async Task<Core.Entities.OAuth.Token> GetOAuthToken(string clientId, string clientSecret, string username, string password, Core.Entities.OAuth.AccessScope scope, CancellationToken? token = null)
        {
            var data = new Dictionary<string, string> {
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "scope", scope.QueryString },
                { "grant_type", "password" },
                { "username", username },
                { "password", password }
            };
            var response = await PostAsync("/oauth/token", data, null, token);
            return await response
                .Content.ReadAsStringAsync()
                .ContinueWith((task) => JsonConvert.DeserializeObject<Core.Entities.OAuth.Token>(task.Result));
        }
    }
}
