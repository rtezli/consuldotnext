using Pixills.Net.Http;
using System.Threading.Tasks;
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
        }

        [Fact]
        public async Task GetServices_QueryWithExistingService_ShouldReturnTheRegisteredServices()
        {
            var client = new Consul.Client.Client(CreateHttpConnection());
            var services = await client.GetService("test-service");
        }

        [Fact]
        public async Task GetServices_QueryWithNonExistingService_ShouldReturnTheRegisteredServices()
        {
            var client = new Consul.Client.Client(CreateHttpConnection());
            var services = await client.GetService("non-existing-service");
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
