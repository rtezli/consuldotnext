using Pixills.Net.Http;
using System.Threading.Tasks;
using System.Linq;
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
            var services = await client.GetService("test-service");
            Assert.Equal(services, Enumerable.Empty<string, string[]>());
        }

        [Fact]
        public async Task GetServices_QueryWithNonExistingService_ShouldReturnAnEmptyCollection()
        {
            var client = new Consul.Client.Client(CreateHttpConnection());
            var services = await client.GetService("non-existing-service");
            Assert.Equal(services, Enumerable.Empty<string, string[]>());
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
