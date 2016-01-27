using System;
using System.Net.Http;
using Pixills.Net.Http;
using System.Threading.Tasks;

namespace Pixills.Consul.Client.Tests
{
    public class HttpClient : IHttpClient
    {
        public Uri BaseAddress {get; set;}
        public TimeSpan Timeout {get; set;}

        public Task<HttpResponseMessage> GetAsync(string url, HttpCompletionOption options)
        {
            return new Task<HttpResponseMessage>(() => {return new HttpResponseMessage();});
        }

        public Task<HttpResponseMessage> PutAsync(string url, HttpContent content)
        {
            return new Task<HttpResponseMessage>(() => {return new HttpResponseMessage();});
        }

        internal void SetResponse(HttpResponseMessage reponse)
        {

        }

        internal void SetException(Exception exception)
        {

        }

        internal void SetRequestTime(int secondsUntilReturn)
        {

        }

        public void CancelPendingRequests()
        {

        }

        public void Dispose()
        {

        }
    }
}
