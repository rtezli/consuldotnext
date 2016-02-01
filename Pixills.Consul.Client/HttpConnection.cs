using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Pixills.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;

namespace Pixills.Consul.Client
{
    public class HttpConnection
    {
        private readonly IHttpClient _client;
        private readonly MediaTypeHeaderValue _contentType = new MediaTypeHeaderValue("application/json");
        private readonly ProductInfoHeaderValue _userAgent = new ProductInfoHeaderValue("PixillsConsulClient","0.0.1");

        private readonly string _consulHostName = Environment.GetEnvironmentVariable("CONSUL_AGENT_HOSTNAME") ?? "localhost";
        private readonly string _consulPort = Environment.GetEnvironmentVariable("CONSUL_AGENT_PORT") ?? "8500";

        public string ApiVersion { get; } = "v1";

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

        public HttpConnection(IHttpClient client, bool useTls = false, uint timeoutInSeconds = 0)
        {
            if (string.IsNullOrWhiteSpace(_consulHostName))
            {
                throw new ArgumentException("Host or IP can not be emtpy. Check the environment variable 'CONSUL_AGENT_HOSTNAME'");
            }

            if (client == null)
            {
                throw new ArgumentException("Client can not be null");
            }

            _client = client;
            var match = Regex.Match(_consulHostName.ToLower(), "^http(s)?://");
            if (match.Success)
            {
                _consulHostName = _consulHostName.Remove(match.Index, match.Length);
            }

            try
            {
                var port = _consulPort == "80" ? "" :  _consulPort;
                port = port == "443" && useTls ? "" : port;
                if(!string.IsNullOrWhiteSpace(port))
                {
                    port = $":{port}";
                }
                _client.BaseAddress =
                new Uri($"{(useTls ? "https" : "http")}://{_consulHostName}{port}/{ApiVersion}");
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
            var request = new HttpRequestMessage(HttpMethod.Get, $"{ApiVersion}/{url}");
            request.Headers.UserAgent.Clear();
            request.Headers.UserAgent.Add(_userAgent);
            Debug.WriteLine(request);
            return _client
                .SendAsync(request)
                .ContinueWith(t =>
                {
                    var response = t.Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new ClientException(response.ReasonPhrase);
                    }
                    Debug.WriteLine(response);
                    var stringResult = response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(stringResult);
                    return stringResult;
                })
                .ContinueWith(task =>
                {
                    return JsonConvert.DeserializeObject<T>(task.Result);
                });
        }

        public Task Put(string url, object obj)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"{ApiVersion}/{url}");
            request.Headers.UserAgent.Clear();
            request.Headers.UserAgent.Add(_userAgent);
            Debug.WriteLine(request);
            return Task.Factory.StartNew(() => JsonConvert
                .SerializeObject(obj, Formatting.None,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    }))
                     .ContinueWith(task =>
                     {
                         request.Content = new StringContent(task.Result);
                         request.Content.Headers.ContentType = _contentType;
                         return _client.SendAsync(request).Result;
                     })
                     .ContinueWith(task =>
                     {
                         var response = task.Result;
                         Debug.WriteLine(response);
                         if(!response.IsSuccessStatusCode)
                         {
                             throw new ClientException(response.ReasonPhrase);
                         }
                     });
        }

        public void Dispose()
        {
            _client.CancelPendingRequests();
            _client.Dispose();
        }
    }
}
