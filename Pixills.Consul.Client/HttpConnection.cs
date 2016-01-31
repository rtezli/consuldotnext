using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Pixills.Net.Http;

namespace Pixills.Consul.Client
{
    public class HttpConnection
    {
        private readonly IHttpClient _client;
        public string Host { get; } = "localhost";
        public string ApiVersion { get; } = "v1";
        public string ClientVersion { get; } = "0.0.1";

        public uint Timeout
        {
            get
            {
                return (uint)_client.Timeout.Seconds;
            }
        }

        public Uri BaseAddress
        {
            get
            {
                return _client.BaseAddress;
            }
        }

        public HttpConnection(string consulHostOrIp, IHttpClient client, bool useTls = false, uint timeoutInSeconds = 0)
        {
            if (string.IsNullOrWhiteSpace(consulHostOrIp))
            {
                throw new ArgumentException("Host or IP can not be emtpy");
            }

            if (client == null)
            {
                throw new ArgumentException("Client can not be null");
            }

            _client = client;
            var match = Regex.Match(consulHostOrIp.ToLower(), "^http(s)?://");
            if (match.Success)
            {
                consulHostOrIp = consulHostOrIp.Remove(match.Index, match.Length);
            }

            try
            {
                _client.BaseAddress = new Uri($"{(useTls ? "https" : "http")}://{consulHostOrIp}/{ApiVersion}");
            }
            catch (UriFormatException e)
            {
                throw e;
            }

            if (timeoutInSeconds > 0)
            {
                _client.Timeout = TimeSpan.FromSeconds(timeoutInSeconds);
            }
        }

        public Task<T> Get<T>(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("User-Agent", $"Pixills Consul Client {ClientVersion}");
            return _client
                .SendAsync(request)
                .ContinueWith(t =>
                {
                    return t.Result.Content.ReadAsStringAsync().Result;
                })
                .ContinueWith(t =>
                {
                    return JsonConvert.DeserializeObject<T>(t.Result);
                });
        }

        public Task Put(string url, object obj)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Headers.Add("User-Agent", $"Pixills Consul Client {ClientVersion}");
            return Task.Factory.StartNew(() => JsonConvert
                .SerializeObject(obj, Formatting.None,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    }))
                     .ContinueWith(t =>
                     {
                         request.Content = new StringContent(t.Result);
                         return _client.SendAsync(request);
                     });
        }

        public void Dispose()
        {
            _client.CancelPendingRequests();
            _client.Dispose();
        }
    }
}
