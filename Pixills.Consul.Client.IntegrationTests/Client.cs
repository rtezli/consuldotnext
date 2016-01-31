using Pixills.Net.Http;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Pixills.Consul.Client.IntegrationTests
{
    public class Client
    {
        private readonly string _nodeName;
        private readonly string _serviceName;
        private readonly string _datacenterName;
        private readonly string _consulHostName;
        private readonly string _consulPort;
 
        public Client(HttpConnection connection)
        {
            _nodeName = Environment.GetEnvironmentVariable("CONSUL_CLIENT_NODE_NAME") ?? "undefined node";
            _serviceName = Environment.GetEnvironmentVariable("CONSUL_CLIENT_SERVICE_NAME") ?? "undefined service";
            _datacenterName = Environment.GetEnvironmentVariable("CONSUL_CLIENT_DATACENTER_NAME") ?? "dc1";
            _nodeName = Environment.GetEnvironmentVariable("CONSUL_CLIENT_NODE_NAME") ?? "undefined node";
            _consulHostName = Environment.GetEnvironmentVariable("CONSUL_AGENT_HOSTNAME") ?? "localhost";
            _consulPort = Environment.GetEnvironmentVariable("CONSUL_AGENT_PORT") ?? "8500";
        }

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
            return new HttpConnection($"{_consulHostName}:{_consulPort}", new HttpClient(), false, 3);
        }

        private Service ValidService()
        {
            return new Service
            {
                ServiceName = "test-service",
                Tags = new[] { "test", "service" },
                Address = "127.0.0.1",
                Port = 80
            };
        }

        private Service InValidService()
        {
            return new Service
            {
                ServiceName = "test-service",
                Tags = new[] { "test", "service" },
                Address = "127.0.0.1",
                Port = 80
            };
        }
    }
}
