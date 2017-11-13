using Newtonsoft.Json;

namespace Pawotter.Core.Entities
{
    public class Stats
    {
        [JsonProperty(PropertyName = "user_count")]
        public int UserCount { get; set; }
        [JsonProperty(PropertyName = "status_count")]
        public int StatusCount { get; set; }
        [JsonProperty(PropertyName = "domain_count")]
        public int DomainCount { get; set; }

        public Stats(int userCount, int statusCount, int domainCount)
        {
            UserCount = userCount;
            StatusCount = statusCount;
            DomainCount = domainCount;
        }

        public override string ToString() => string.Format("[Stats: UserCount={0}, StatusCount={1}, DomainCount={2}]", UserCount, StatusCount, DomainCount);

        public override bool Equals(object obj)
        {
            var o = obj as Stats;
            if (o == null) return false;
            return Equals(UserCount, o.UserCount) &&
                Equals(StatusCount, o.StatusCount) &&
                Equals(DomainCount, o.DomainCount);
        }

        public override int GetHashCode() => Object.GetHashCode(UserCount) ^
                                             Object.GetHashCode(StatusCount) ^
                                             Object.GetHashCode(DomainCount);
    }
}
