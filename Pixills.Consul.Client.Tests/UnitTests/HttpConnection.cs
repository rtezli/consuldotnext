using Xunit;
using Pixills.Net.Http;
using Pixills.Consul.Client;

namespace Pixills.Consul.Client.Tests
{
    public class HttpConnection
    {
        [Fact]
        public void HttpConnection_Constructor_CallWithValidArgs_ShouldNotThrowException()
        {
            Assert.Equal(true, false);
        }

        [Fact]
        public void HttpConnection_Constructor_CallWithLeastParameters_ShouldNotThrowException()
        {
            Assert.Equal(true, false);
        }

        [Fact]
        public void HttpConnection_Constructor_CallWithHostWithHttpPrefix_ShouldRemoveHttpPrefix()
        {
            Assert.Equal(true, false);
        }

        [Fact]
        public void HttpConnection_Constructor_CallWithTlsFalse_ShouldAddHttpPrefix()
        {
            Assert.Equal(true, false);
        }

        [Fact]
        public void HttpConnection_Constructor_CallWithTlsTrue_ShouldAddHttpsPrefix()
        {
            Assert.Equal(true, false);
        }

        [Fact]
        public void HttpConnection_Constructor_CallWithTimeout0_ShouldNotAddTimeout()
        {
            Assert.Equal(true, false);
        }

        [Fact]
        public void HttpConnection_Constructor_CallWithTimeoutGreater0_ShouldAddTimeout()
        {
            Assert.Equal(true, false);
        }
    }
}
