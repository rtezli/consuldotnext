using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Pixills.Net.Http;

namespace Pixills.Consul.Client
{
    public class HttpConnection
    {
        public IHttpClient Client { get; }
        public string Host { get; } = "localhost";
        public string ApiVersion { get; } = "v1";

        public HttpConnection(string consulHostOrIp, IHttpClient client, bool useTls = false, uint timeoutInSeconds = 0)
        {
            Client = client;
            var match = Regex.Match(consulHostOrIp.ToLower(),"^http(s)?://");
            if (match.Success)
            {
                consulHostOrIp = consulHostOrIp.Remove(match.Index, match.Length);
            }
            if (timeoutInSeconds > 0)
            {
                Client.Timeout = TimeSpan.FromSeconds(timeoutInSeconds);
            }
            Client.BaseAddress = new Uri($"{(useTls ? "https" : "http")}://{consulHostOrIp}/{ApiVersion}");
        }

        public async Task<T> Get<T>(string url)
        {
            var option = new HttpCompletionOption();
            var response = await Client.GetAsync(url, option);
            var contentString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(contentString);
        }

        public Task Put(string url, object obj)
        {
            return new Task(async () =>
            {
                await Client.PutAsync(url, new StringContent(Serializer.Serialize(obj)));
            });
        }

        public void Dispose()
        {
            Client.CancelPendingRequests();
            Client.Dispose();
        }
    }
}
