using Xunit;
using Pixills.Consul.Client;
using Pixills.Consul.Client.Tests.Mocks;

namespace Pixills.Consul.Client.Tests
{
    public class ClientTests
    {
        [Fact]
        public void ConstructorTest()
        {
            var http = new MockHttpClient();
            var client = new Client(http, "localhost", "testservice");
        }

        [Fact]
        public void FailingTest()
        {

        }
    }
}
