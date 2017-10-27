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

        /// <summary>
        /// Initializes for JSON.NET
        /// </summary>
        internal Instance() { }

        public Instance(string uri, string title, string description, string email)
        {
            Uri = uri;
            Title = title;
            Description = description;
            Email = email;
        }

        public override string ToString() => string.Format("[Instance: Uri={0}, Title={1}, Description={2}, Email={3}]", Uri, Title, Description, Email);

        public override bool Equals(object obj)
        {
            var o = obj as Instance;
            if (o == null) return false;
            return Equals(Uri, o.Uri) &&
                Equals(Title, o.Title) &&
                Equals(Description, o.Description) &&
                Equals(Email, o.Email);
        }

        public override int GetHashCode() => Object.GetHashCode(Uri) ^
                         Object.GetHashCode(Title) ^
                         Object.GetHashCode(Description) ^
                         Object.GetHashCode(Email);
    }
}
