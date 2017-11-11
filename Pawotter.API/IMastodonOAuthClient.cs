using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pawotter.API
{
    public interface IMastodonOAuthClient
    {
        /// <summary>
        /// Creates the app on Mastodon instance.
        /// </summary>
        /// <returns>MastodonApp.</returns>
        /// <param name="clientName">Client name.</param>
        /// <param name="redirectUris">Redirect uris.</param>
        /// <param name="scope">Scope.</param>
        /// <param name="token">Token.</param>
        Task<Core.Entities.OAuth.Application> CreateApp(string clientName, Uri redirectUris, Core.Entities.OAuth.AccessScope scope, CancellationToken? token = null);

        /// <summary>
        /// Return an OAuth token.
        /// DO NOT USE IN APP.
        /// </summary>
        /// <returns>OAuth token.</returns>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        /// <param name="scope">Scope.</param>
        /// <param name="token">Token.</param>
        Task<Core.Entities.OAuth.Token> GetOAuthToken(string clientId, string clientSecret, string username, string password, Core.Entities.OAuth.AccessScope scope, CancellationToken? token = null);

        /// <summary>
        /// If you have neither refresh nor access token, tell the user to visit this uri.
        /// https://github.com/tootsuite/documentation/blob/master/Using-the-API/OAuth-details.md
        /// </summary>
        /// <returns>The OA uth authorize URI.</returns>
        /// <param name="redirectUris">Redirect uris.</param>
        /// <param name="scope">Scope.</param>
        /// <param name="clientId">Client identifier.</param>
        Uri GetOAuthAuthorizeUri(Uri redirectUris, Core.Entities.OAuth.AccessScope scope, string clientId);
    }
}
