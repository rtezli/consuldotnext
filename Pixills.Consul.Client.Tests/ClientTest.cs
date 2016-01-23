using Xunit;
using Pixills.Consul.Client;
using Pixills.Consul.Client.Tests.Mocks;

namespace Pixills.Consul.Client.Tests
{
    public class ClientTests
    {
        private MockHttpClient _mockHttpClient;

        [Fact]
        public void Constructor_WhenPassingValidArgs()
        {
            _mockHttpClient = new MockHttpClient();
            var client = new Client("localhost", _mockHttpClient, "localhost", "testservice");
        }

        [Fact]
        public void Constructor_WhenPassingAnIvalidBaseUrl()
        {
            _mockHttpClient = new MockHttpClient();
            var client = new Client("localhost", _mockHttpClient, "localhost", "testservice");
        }

        [Fact]
        public void Constructor_WhenServiceNameIsInvalidOrEmpty()
        {
            _mockHttpClient = new MockHttpClient();
            var client = new Client("localhost", _mockHttpClient, "localhost", "testservice");
        }

        [Fact]
        public void GetServices_WhenServiceNameIsInvalidOrEmpty()
        {
            var client = new Client("localhost", _mockHttpClient, "localhost", "testservice");
            var services = client.Services();
        }
    }
}
