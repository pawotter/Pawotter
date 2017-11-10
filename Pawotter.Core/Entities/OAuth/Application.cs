using System;
using Newtonsoft.Json;

namespace Pawotter.Core.Entities.OAuth
{
    public class Application
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "client_id")]
        public string ClientId { get; set; }
        [JsonProperty(PropertyName = "client_secret")]
        public string ClientSecret { get; set; }
        [JsonProperty(PropertyName = "redirect_uri")]
        public Uri RedirectUri { get; set; }

        internal Application() { }

        public Application(string id, string clientId, string clientSecret, Uri redirectUri)
        {
            Id = id;
            ClientId = clientId;
            ClientSecret = clientSecret;
            RedirectUri = redirectUri;
        }

        public override bool Equals(object obj)
        {
            var o = obj as Application;
            if (o == null) return false;
            return Equals(Id, o.Id) &&
                Equals(ClientId, o.ClientId) &&
                Equals(ClientSecret, o.ClientSecret) &&
                Equals(RedirectUri, o.RedirectUri);
        }

        public override int GetHashCode() => Object.GetHashCode(Id, ClientId, ClientSecret, RedirectUri);

        public override string ToString() => string.Format("[MastodonApp: Id={0}, ClientId={1}, ClientSecret={2}, RedirectUri={3}]", Id, ClientId, ClientSecret, RedirectUri);
    }
}
