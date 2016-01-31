using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pixills.Net.Http
{
    public interface IHttpClient : IDisposable
    {
        Uri BaseAddress {get; set;}
        TimeSpan Timeout {get; set;}
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
        void CancelPendingRequests();
    }
}
