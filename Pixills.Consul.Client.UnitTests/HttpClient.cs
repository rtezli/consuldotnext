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

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return new Task<HttpResponseMessage>(() => 
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            });
        }

        public void CancelPendingRequests()
        {

        }

        public void Dispose()
        {

        }
    }
}
