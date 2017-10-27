using System.Collections.Generic;
using Newtonsoft.Json;

namespace Pawotter.Core.Entities
{
    /// <summary>
    /// Context.
    /// https://github.com/tootsuite/documentation/blob/master/Using-the-API/API.md#card
    /// </summary>
    public class Context
    {
        [JsonProperty(PropertyName = "ancestors")]
        public IList<Status> Ancestors { get; set; }
        [JsonProperty(PropertyName = "descendants")]
        public IList<Status> Descendants { get; set; }

        /// <summary>
        /// Initializes for JSON.NET
        /// </summary>
        internal Context() { }

        public Context(IList<Status> ancestors, IList<Status> descendants)
        {
            Ancestors = ancestors;
            Descendants = descendants;
        }

        public override string ToString() => string.Format("[Context: Ancestors={0}, Descendants={1}]", Ancestors, Descendants);

        public override bool Equals(object obj)
        {
            var o = obj as Context;
            if (o == null) return false;
            return Object.SequenceEqual(Ancestors, o.Ancestors) &&
                         Object.SequenceEqual(Descendants, o.Descendants);
        }

        public override int GetHashCode() => Object.GetHashCode(Ancestors, Descendants);
    }
}
