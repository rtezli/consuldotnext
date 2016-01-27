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

        }

        [Fact]
        public void HttpConnection_Constructor_CallWithLeastParameters_ShouldNotThrowException()
        {

        }

        [Fact]
        public void HttpConnection_Constructor_CallWithHostWithHttpPrefix_ShouldRemoveHttpPrefix()
        {

        }

        [Fact]
        public void HttpConnection_Constructor_CallWithTlsFalse_ShouldAddHttpPrefix()
        {

        }

        [Fact]
        public void HttpConnection_Constructor_CallWithTlsTrue_ShouldAddHttpsPrefix()
        {

        }

        [Fact]
        public void HttpConnection_Constructor_CallWithTimeout0_ShouldNotAddTimeout()
        {

        }

        [Fact]
        public void HttpConnection_Constructor_CallWithTimeoutGreater0_ShouldAddTimeout()
        {

        }
    }
}
