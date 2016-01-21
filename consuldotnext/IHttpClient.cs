using System;
using System.Threading.Tasks;

namespace Consul
{
  public interface IHttpClient : IDisposable
  {
    Task<T> Get<T>(string url);
    T GetSync<T>(string url);
    Task Put(string url, object obj);
    void PutSync(string url, object obj);
  }
}
