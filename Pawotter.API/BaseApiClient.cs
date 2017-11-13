using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Pawotter.API
{
    public abstract class BaseApiClient
    {
        protected readonly Uri baseUri;
        protected readonly HttpClient http;

        protected BaseApiClient(Uri baseUri, HttpClient http)
        {
            this.baseUri = baseUri;
            this.http = http;
        }

        protected async Task<HttpResponseMessage> GetAsync(string path, IEnumerable<KeyValuePair<string, object>> parameters = null, Dictionary<string, string> headers = null, CancellationToken? token = null)
        {
            var url = CreateUrl(baseUri, path, parameters);
            var request = CreateRequest(HttpMethod.Get, url, headers);
            var response = token.HasValue ? await http.SendAsync(request, token.Value) : await http.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return response;
        }

        protected Task<HttpResponseMessage> PostAsync(string path, Dictionary<string, string> parameters = null, Dictionary<string, string> headers = null, CancellationToken? token = null)
        {
            return SendFormUrlEncodedContentAsyc(HttpMethod.Post, path, parameters, headers, token);
        }

        protected Task<HttpResponseMessage> PatchAsync(string path, Dictionary<string, string> parameters = null, Dictionary<string, string> headers = null, CancellationToken? token = null)
        {
            return SendFormUrlEncodedContentAsyc(new HttpMethod("PATCH"), path, parameters, headers, token);
        }

        protected Task<HttpResponseMessage> DeleteAsync(string path, Dictionary<string, string> parameters = null, Dictionary<string, string> headers = null, CancellationToken? token = null)
        {
            return SendFormUrlEncodedContentAsyc(HttpMethod.Delete, path, parameters, headers, token);
        }

        async Task<HttpResponseMessage> SendFormUrlEncodedContentAsyc(HttpMethod method, string path, Dictionary<string, string> parameters = null, Dictionary<string, string> headers = null, CancellationToken? token = null)
        {
            var url = CreateUrl(baseUri, path, null);
            var request = CreateRequest(method, url, headers);
            if (parameters != null) request.Content = new FormUrlEncodedContent(parameters);
            var response = token.HasValue ? await http.SendAsync(request, token.Value) : await http.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return response;
        }

        protected static HttpRequestMessage CreateRequest(HttpMethod method, Uri url, Dictionary<string, string> headers = null)
        {
            var request = new HttpRequestMessage(method, url);
            if (headers == null) return request;
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
            return request;
        }

        protected static Uri CreateUrl(Uri baseUri, string path, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var p = path ?? "";
            var q = parameters?.AsQueryString() ?? "";
            return new Uri(baseUri, $"{p}{q}");
        }

        public override string ToString() => string.Format("[BaseApiClient: baseUrl={0}, http={1}]", baseUri, http);

        public override bool Equals(object obj)
        {
            var o = obj as BaseApiClient;
            if (o == null) return false;
            return Equals(baseUri, o.baseUri) &&
                Equals(http, o.http);
        }

        public override int GetHashCode() => Core.Object.GetHashCode(baseUri, http);
    }
}
