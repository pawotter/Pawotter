using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Pawotter.API
{
    public class BaseApiClient
    {
        readonly Uri baseUrl;
        readonly HttpClient http;

        protected BaseApiClient(Uri baseUrl, HttpClient http)
        {
            this.baseUrl = baseUrl;
            this.http = http;
        }

        protected async Task<HttpResponseMessage> GetAsync(string path, IEnumerable<KeyValuePair<string, object>> parameters = null, Dictionary<string, string> headers = null, CancellationToken? token = null)
        {
            var url = CreateUrl(baseUrl, path, parameters);
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
            var url = CreateUrl(baseUrl, path, null);
            var request = CreateRequest(method, url, headers);
            if (parameters != null) request.Content = new FormUrlEncodedContent(parameters);
            var response = token.HasValue ? await http.SendAsync(request, token.Value) : await http.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return response;
        }

        HttpRequestMessage CreateRequest(HttpMethod method, Uri url, Dictionary<string, string> headers = null)
        {
            var request = new HttpRequestMessage(method, url);
            if (headers == null) return request;
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
            return request;
        }

        static Uri CreateUrl(Uri baseUri, string path, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var p = path ?? "";
            var q = parameters?.AsQueryString() ?? "";
            return new Uri(baseUri, $"{p}{q}");
        }

        public override string ToString() => string.Format("[BaseApiClient: baseUrl={0}, http={1}]", baseUrl, http);

        public override bool Equals(object obj)
        {
            var o = obj as BaseApiClient;
            if (o == null) return false;
            return Equals(baseUrl, o.baseUrl) &&
                Equals(http, o.http);
        }

        public override int GetHashCode() => Core.Object.GetHashCode(baseUrl, http);
    }
}
