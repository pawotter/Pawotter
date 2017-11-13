using Newtonsoft.Json;

namespace Pawotter.Core.Entities
{
    /// <summary>
    /// Instance.
    /// https://github.com/tootsuite/documentation/blob/master/Using-the-API/API.md#instance
    /// </summary>
    public class Instance
    {
        [JsonProperty(PropertyName = "uri")]
        public string Uri { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "version")]
        public string Version { get; set; }
        [JsonProperty(PropertyName = "stats")]
        public Stats Stats { get; set; }

        public Instance(string uri, string title, string description, string email, string version, Stats stats)
        {
            Uri = uri;
            Title = title;
            Description = description;
            Email = email;
            Version = version;
            Stats = stats;
        }

        public override string ToString() => string.Format("[Instance: Uri={0}, Title={1}, Description={2}, Email={3}, Version={4}, Stats={5}]", Uri, Title, Description, Email, Version, Stats);

        public override bool Equals(object obj)
        {
            var o = obj as Instance;
            if (o == null) return false;
            return Equals(Uri, o.Uri) &&
                Equals(Title, o.Title) &&
                Equals(Description, o.Description) &&
                Equals(Email, o.Email) &&
                Equals(Version, o.Version) &&
                Equals(Stats, o.Stats);
        }

        public override int GetHashCode() => Object.GetHashCode(Uri) ^
                                             Object.GetHashCode(Title) ^
                                             Object.GetHashCode(Description) ^
                                             Object.GetHashCode(Email) ^
                                             Object.GetHashCode(Version) ^
                                             Object.GetHashCode(Stats);
    }
}
