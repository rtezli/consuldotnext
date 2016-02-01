using Pixills.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Xunit;

namespace Pixills.Consul.Client.IntegrationTests
{
    public class Client
    {
        [Fact]
        public async Task Register_RegisterAValidService_ShouldRegisterAService()
        {
            var client = new Consul.Client.Client(CreateHttpConnection());
            await client.Register(ValidService());
            var service = await client.GetService("test-service");
            Assert.Equal("test-service", service[0].Name);
        }

        [Fact]
        public async Task GetServices_QueryWithNonExistingService_ShouldReturnAnEmptyCollection()
        {
            var client = new Consul.Client.Client(CreateHttpConnection());
            var service = await client.GetService("non-existing-service");
            Assert.Equal(new Service[0], service);
        }

        private HttpConnection CreateHttpConnection()
        {
            return new HttpConnection(new HttpClient(), false, 3);
        }

        private Service ValidService()
        {
            return new Service
            {
                Name = "test-service",
                Tags = new[] { "test", "service" },
                Address = "127.0.0.1",
                Port = 80
            };
        }

        private Service InValidService()
        {
            return new Service
            {
                Name = "test-service",
                Tags = new[] { "test", "service" },
                Address = "127.0.0.1",
                Port = 80
            };
        }
    }
}
