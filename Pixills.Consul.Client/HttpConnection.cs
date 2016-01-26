using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Pixills.Consul.Client
{
    public class HttpConnection
    {
        public HttpClient Client { get; } = new HttpClient();
        public string Host { get; } = "localhost";
        public string ApiVersion { get; } = "v1";

        public HttpConnection(string consulHostOrIp, bool useTls = false, uint timeoutInSeconds = 0)
        {
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

        public T GetSync<T>(string url)
        {
            var option = new HttpCompletionOption();
            var response = Client.GetAsync(url, option).Result;
            var contentString = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(contentString);
        }

        public Task Put(string url, object obj)
        {
            return new Task(async () =>
            {
                await Client.PutAsync(url, new StringContent(Serializer.Serialize(obj)));
            });
        }

        public void PutSync(string url, object obj)
        {
            var response = Client.PutAsync(url, new StringContent(Serializer.Serialize(obj))).Result;
        }

        public void Dispose()
        {
            Client.CancelPendingRequests();
            Client.Dispose();
        }
    }
}
