using System;
using System.Threading;
using System.Threading.Tasks;
using Pawotter.Core.Entities;
using System.Net.Http;
using Newtonsoft.Json;

namespace Pawotter.API
{
    public class MastodonApiClient : BaseApiClient, IMastodonApiClient
    {
        public MastodonApiClient(Uri baseUri, HttpClient http) : base(baseUri, http) { }

        public Task AuthorizeFollowRequestsAsync(string id, CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Relationship> BlockAsync(string id, CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task ClearNotificationsAsync(CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task DeleteStatusAsync(string id, CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Status> FavouriteAsync(string id, CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Relationship> FollowAsync(string id, CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Account> FollowRemoteUserAsync(string uri, CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Account> GetAccountAsync(string id, CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Response<Account[]>> GetBlocksAsync(Link? link = default(Link?), CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Card> GetCardAsync(string id, CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Context> GetContextAsync(string id, CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Account> GetCurrentAccountAsync(CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Response<Account[]>> GetFavouritedByAsync(string id, CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Response<Status[]>> GetFavouritesAsync(Link? link = default(Link?), CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Response<Account[]>> GetFollowersAsync(string id, int? limit = default(int?), Link? link = default(Link?), CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Response<Account[]>> GetFollowingAsync(string id, int? limit = default(int?), Link? link = default(Link?), CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Response<Account[]>> GetFollowRequestsAsync(Link? link = default(Link?), CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Response<Status[]>> GetHomeTimelinesAsync(Link? link = default(Link?), CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public async Task<Instance> GetInstanceAsync(Uri instanceUri = null, CancellationToken? token = default(CancellationToken?))
        {
            var url = CreateUrl(instanceUri ?? baseUri, "/api/v1/instance", null);
            var request = CreateRequest(HttpMethod.Get, url, null);
            var response = token.HasValue ? await http.SendAsync(request, token.Value) : await http.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response
                    .Content.ReadAsStringAsync()
                    .ContinueWith((task) => JsonConvert.DeserializeObject<Instance>(task.Result));
        }

        public Task<Response<Account[]>> GetMutesAsync(Link? link = default(Link?), CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Notification> GetNotificationAsync(string id, CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Response<Notification[]>> GetNotificationsAsync(Link? link = default(Link?), CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Response<Status[]>> GetPublicTimelinesAsync(bool? isLocal = default(bool?), Link? link = default(Link?), CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Response<Account[]>> GetRebloggedByAsync(string id, CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Relationship> GetRelationshipAsync(string id, CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Relationship[]> GetRelationshipsAsync(string[] ids, CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Response<Report[]>> GetReportsAsync(Link? link = default(Link?), CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Status> GetStatusAsync(string id, CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Response<Status[]>> GetStatusesAsync(string id, bool isOnlyMedia = false, bool isExcludeReplies = false, int? limit = default(int?), Link? link = default(Link?), CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Response<Status[]>> GetTagTimelinesAsync(string hashtag, bool? isLocal = default(bool?), Link? link = default(Link?), CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Relationship> MuteAsync(string id, CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Status> PostStatusAsync(string status, string inReplyTo = null, string mediaId = null, string spoilerText = null, StatusVisibility? visibility = default(StatusVisibility?), bool? isSensitive = default(bool?), CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Status> ReblogAsync(string id, CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task RejectFollowRequestsAsync(string id, CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Report> ReportAsync(string accountId, string statusId, string comment, CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Response<Account[]>> SearchAccountsAsync(string query, int? limit = default(int?), Link? link = default(Link?), CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Response<Results>> SearchAsync(string query, bool resolve = true, int? limit = default(int?), Link? link = default(Link?), CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Relationship> UnblockAsync(string id, CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Status> UnfavouriteAsync(string id, CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Relationship> UnfollowAsync(string id, CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Relationship> UnmuteAsync(string id, CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Status> UnreblogAsync(string id, CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Account> UpdateCurrentAccountAsync(string displayName = null, string note = null, string base64EncodedAvater = null, string base64EncodedHeader = null, CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }

        public Task<Attachment> UploadAsync(string base64EncodedMedia, CancellationToken? token = default(CancellationToken?))
        {
            throw new NotImplementedException();
        }
    }
}
