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

        public HttpClient()
        {

        }

        public HttpClient(HttpResponseMessage reponse)
        {

        }

        public HttpClient(Exception exception)
        {

        }

        public HttpClient(int timeout)
        {

        }

        public Task<HttpResponseMessage> GetAsync(string url, HttpCompletionOption options)
        {
            return new Task<HttpResponseMessage>(() => {return new HttpResponseMessage();});
        }

        public Task<HttpResponseMessage> PutAsync(string url, HttpContent content)
        {
            return new Task<HttpResponseMessage>(() => {return new HttpResponseMessage();});
        }

        public void CancelPendingRequests()
        {

        }

        public void Dispose()
        {

        }
    }
}
