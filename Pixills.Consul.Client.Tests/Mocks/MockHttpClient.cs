using System;
using Xunit;
using System.Threading.Tasks;
using Pixills.Consul.Client;

namespace Pixills.Consul.Client.Tests.Mocks
{
    public class MockHttpClient : IHttpClient
    {
        private int _latency = 0;

        public Task<T> Get<T>(string url)
        {
            Task.Delay(_latency);
            var t = (T)Activator.CreateInstance(typeof(T));
            return new Task<T>(() => {return t;});
        }

        public T GetSync<T>(string url)
        {
            return default(T);
        }

        public Task Put(string url, object obj)
        {
            Task.Delay(_latency);
            return new Task(() => {});
        }

        public void PutSync(string url, object obj)
        {

        }

        public void SetLatency(int l)
        {
            _latency = l;
        }

        public void Dispose()
        {

        }
    }
}
