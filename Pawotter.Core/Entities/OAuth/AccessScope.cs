using System.Collections.Generic;
using System.Linq;

namespace Pawotter.Core.Entities.OAuth
{
    public enum AccessScopeType
    {
        Read,
        Write,
        Follow
    }

    public class AccessScope
    {
        IEnumerable<AccessScopeType> types;

        public AccessScope(params AccessScopeType[] types)
        {
            this.types = types.ToList();
        }

        public string QueryString => string.Join(" ", types.Distinct().Select(x => GetQueryString(x)));

        static string GetQueryString(AccessScopeType type)
        {
            switch (type)
            {
                case AccessScopeType.Read: return "read";
                case AccessScopeType.Write: return "write";
                case AccessScopeType.Follow: return "follow";
            }
            return "";
        }
    }
}
