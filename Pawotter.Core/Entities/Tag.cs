using System;
using Newtonsoft.Json;

namespace Pawotter.Core.Entities
{
    /// <summary>
    /// Tag.
    /// https://github.com/tootsuite/documentation/blob/master/Using-the-API/API.md#tag
    /// </summary>
    public class Tag
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "url")]
        public Uri Url { get; set; }

        /// <summary>
        /// Initializes for JSON.NET
        /// </summary>
        internal Tag() { }

        public Tag(string name, Uri url)
        {
            Name = name;
            Url = url;
        }

        public override string ToString() => string.Format("[Tag: Name={0}, Url={1}]", Name, Url);

        public override bool Equals(object obj)
        {
            var o = obj as Tag;
            if (o == null) return false;
            return Equals(Name, o.Name) &&
                Equals(Url, o.Url);
        }

        public override int GetHashCode() => Object.GetHashCode(Name, Url);
    }
}
