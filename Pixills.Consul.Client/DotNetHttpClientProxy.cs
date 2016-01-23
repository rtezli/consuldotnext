using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace Pixills.Consul.Client
{
    internal class DotNetHttpClientProxy : IHttpClient
    {
        private readonly HttpClient _client;

        public DotNetHttpClientProxy()
        {
            _client = new HttpClient();
        }

        public async Task<T> Get<T>(string url)
        {
            var option = new HttpCompletionOption();
            var response = await _client.GetAsync(url, option);
            var contentString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(contentString);
        }

        public T GetSync<T>(string url)
        {
            var option = new HttpCompletionOption();
            var response = _client.GetAsync(url, option).Result;
            var contentString = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(contentString);
        }

        public Task Put(string url, object obj)
        {
            return new Task(async () =>
            {
                await _client.PutAsync(url, new StringContent(Serializer.Serialize(obj)));
            });
        }

        public void PutSync(string url, object obj)
        {
            var response = _client.PutAsync(url, new StringContent(Serializer.Serialize(obj))).Result;
        }

        public void Dispose()
        {
            _client.CancelPendingRequests();
            _client.Dispose();
        }
    }
}
