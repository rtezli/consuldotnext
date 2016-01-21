using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace Consul
{
  public class DotNetHttpClientProxy : IHttpClient
  {
    private readonly HttpClient _client;

    public DotNetHttpClientProxy(){
      _client = new HttpClient();
    }
      public async Task<T> Get<T>(string url){
          var option = new HttpCompletionOption();
          var response = await _client.GetAsync(url, option);
          var contentString = await response.Content.ReadAsStringAsync();
          return JsonConvert.DeserializeObject<T>(contentString);
      }

      public T GetSync<T>(string url){
          var option = new HttpCompletionOption();
          var response = _client.GetAsync(url, option).Result;
          var contentString = response.Content.ReadAsStringAsync().Result;
          return JsonConvert.DeserializeObject<T>(contentString);
      }

      public Task Put(string url, object obj){
          return new Task(async () => {
              var contentString =  JsonConvert.SerializeObject(obj);
              await _client.PutAsync(url, new StringContent(contentString));
            });
      }

      public void PutSync(string url, object obj){
          var contentString =  JsonConvert.SerializeObject(obj);
          var response = _client.PutAsync(url, new StringContent(contentString)).Result;
      }

      public void Dispose(){
        _client.CancelPendingRequests();
        _client.Dispose();
      }
  }
}
