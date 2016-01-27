using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pixills.Net.Http
{
    public interface IHttpClient : IDisposable
    {
        Uri BaseAddress {get; set;}
        TimeSpan Timeout {get; set;}
        Task<HttpResponseMessage> GetAsync(string url, HttpCompletionOption options);
        Task<HttpResponseMessage> PutAsync(string url, HttpContent content);
        void CancelPendingRequests();
    }
}
