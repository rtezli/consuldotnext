using Xunit;
using Pixills.Net.Http;
using System;

namespace Pixills.Consul.Client.UnitTests
{
    public class HttpConnection
    {
        private IHttpClient _client;

        public HttpConnection()
        {
            _client = new HttpClient();
            Environment.SetEnvironmentVariable("CONSUL_AGENT_PORT", "1337");
        }

        [Fact]
        public void HttpConnection_Constructor_CallWithInvalidUrl_ShouldThrowException()
        {
            Assert.Throws<UriFormatException>(() =>
            {
                Environment.SetEnvironmentVariable("CONSUL_AGENT_HOSTNAME", "invalid url");
                new Consul.Client.HttpConnection(_client, true, 5);
            });
        }

        [Fact]
        public void HttpConnection_Constructor_CallWithNullUrl_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Environment.SetEnvironmentVariable("CONSUL_AGENT_HOSTNAME", null);
                new Consul.Client.HttpConnection(_client, true, 5);
            });
        }

        [Fact]
        public void HttpConnection_Constructor_CallWithNullClient_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Environment.SetEnvironmentVariable("CONSUL_AGENT_HOSTNAME", null);
                new Consul.Client.HttpConnection(null, true, 5);
            });
        }

        [Fact]
        public void HttpConnection_Constructor_CallWithValidArgs_ShouldNotThrowException()
        {
            Environment.SetEnvironmentVariable("CONSUL_AGENT_HOSTNAME", "localhost");
            var con = new Consul.Client.HttpConnection(_client, true, 5);
        }

        [Fact]
        public void HttpConnection_Constructor_CallWithLeastParameters_ShouldNotThrowException()
        {
            Environment.SetEnvironmentVariable("CONSUL_AGENT_HOSTNAME", "localhost");
            var con = new Consul.Client.HttpConnection(_client);
        }

        [Fact]
        public void HttpConnection_Constructor_CallWithHostWithHttpPrefix_ShouldRemoveHttpPrefix()
        {
            Environment.SetEnvironmentVariable("CONSUL_AGENT_HOSTNAME", "http://localhost");
            var con = new Consul.Client.HttpConnection(_client);
            Assert.Equal(new Uri("http://localhost:1337/v1"), con.BaseAddress);
        }

        [Fact]
        public void HttpConnection_Constructor_CallWithTlsFalse_ShouldAddHttpPrefix()
        {
            Environment.SetEnvironmentVariable("CONSUL_AGENT_HOSTNAME", "localhost");
            var con = new Consul.Client.HttpConnection(_client);
            Assert.Equal(new Uri("http://localhost:1337/v1"), con.BaseAddress);
        }

        [Fact]
        public void HttpConnection_Constructor_CallWithTlsTrue_ShouldAddHttpsPrefix()
        {
            Environment.SetEnvironmentVariable("CONSUL_AGENT_HOSTNAME", "localhost");
            var con = new Consul.Client.HttpConnection(_client, true);
            Assert.Equal(new Uri("https://localhost:1337/v1"), con.BaseAddress);
        }

        [Fact]
        public void HttpConnection_Constructor_CallWithTimeout0_ShouldNotAddTimeout()
        {
            Environment.SetEnvironmentVariable("CONSUL_AGENT_HOSTNAME", "localhost");
            var con = new Consul.Client.HttpConnection(_client, true, 0);
            Assert.Equal(0, (int)con.Timeout);
        }

        [Fact]
        public void HttpConnection_Constructor_CallWithTimeoutGreater0_ShouldAddTimeout()
        {
            Environment.SetEnvironmentVariable("CONSUL_AGENT_HOSTNAME", "localhost");
            var con = new Consul.Client.HttpConnection(_client, true, 5);
            Assert.Equal(5, (int)con.Timeout);
        }
    }
}
