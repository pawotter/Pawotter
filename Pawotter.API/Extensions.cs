using System.Collections.Generic;
using System.Linq;
using System.Net;
using System;

namespace Pawotter.API
{
    public static class Extensions
    {
        internal static string AsQueryString(this IDictionary<string, object> parameters)
        {
            if (parameters == null || !parameters.Any()) return "";
            var enumerable = parameters.Select(x => new KeyValuePair<string, object>(x.Key, x.Value));
            return enumerable.AsQueryString();
        }

        internal static string AsQueryString(this IEnumerable<KeyValuePair<string, object>> parameters)
        {
            if (parameters == null | !parameters.Any()) return "";
            var strings = parameters
                .Select(param => string.Format("{0}={1}", param.Key.UrlEncoded(), param.Value.UrlEncoded()));
            return "?" + string.Join("&", strings);
        }

        internal static string UrlEncoded(this object obj) => WebUtility.UrlEncode(obj.ToString());

        internal static IDictionary<string, string> GetQueryParameters(this Uri uri)
        {
            if (uri == null) return new Dictionary<string, string>();
            return uri.Query
                 .Split("?&".ToCharArray())
                 .Where(x => !string.IsNullOrEmpty(x))
                 .Select(x => x.Split('='))
                 .ToDictionary(x => Uri.UnescapeDataString(x[0]), x => Uri.UnescapeDataString(x[1]));
        }
    }
}
