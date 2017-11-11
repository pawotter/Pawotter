using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pawotter.API
{
    /// <summary>
    /// Mastodon OAuth API client.
    /// https://github.com/tootsuite/documentation/blob/cafdf092bed30410b706f30fdc4f43c698a9f967/Using-the-API/OAuth-details.md
    /// </summary>
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
        /// Return an OAuth Token by using refresh token.
        /// </summary>
        /// <returns>The OA uth token by refresh token.</returns>
        /// <param name="clientId">Client identifier.</param>
        /// <param name="clientSecret">Client secret.</param>
        /// <param name="refreshToken">Refresh token.</param>
        /// <param name="token">Token.</param>
        Task<Core.Entities.OAuth.Token> GetOAuthToken(string clientId, string clientSecret, string refreshToken, CancellationToken? token = null);

        /// <summary>
        /// [DO NOT USE IN APP] Return an OAuth token by using passowrd.
        /// </summary>
        /// <returns>OAuth token.</returns>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        /// <param name="scope">Scope.</param>
        /// <param name="token">Token.</param>
        Task<Core.Entities.OAuth.Token> GetOAuthTokenByPassword(string clientId, string clientSecret, string username, string password, Core.Entities.OAuth.AccessScope scope, CancellationToken? token = null);

        /// <summary>
        /// If you have neither refresh nor access token, tell the user to visit this uri.
        /// </summary>
        /// <returns>The OA uth authorize URI.</returns>
        /// <param name="redirectUris">Redirect uris.</param>
        /// <param name="scope">Scope.</param>
        /// <param name="clientId">Client identifier.</param>
        Uri GetOAuthAuthorizeUri(Uri redirectUris, Core.Entities.OAuth.AccessScope scope, string clientId);
    }
}
