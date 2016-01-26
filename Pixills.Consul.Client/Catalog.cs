using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Pixills.Consul.Client
{
    public class Catalog
    {
        private readonly HttpConnection _connection;

        public Catalog(HttpConnection con)
        {
            _connection = con;
        }

        public Task<string[]> Datacenters()
        {
            var address = string.Format("{0}/catalog/datacenters", _connection.Host);
            return _connection.Get<string[]>(address);
        }

        public Task<Dictionary<string, string[]>> Services()
        {
            var address = string.Format("{0}/catalog/services", _connection.Host);
            return _connection.Get<Dictionary<string, string[]>>(address);
        }

        public Task<Dictionary<string, string[]>> Services(string dataCenter)
        {
            var address = string.Format("{0}/catalog/services?dc={2}", _connection.Host, WebUtility.UrlEncode(dataCenter));
            return _connection.Get<Dictionary<string, string[]>>(address);
        }

        public Task<Service> Service(string name)
        {
            var address = string.Format("{0}/catalog/service/{2}", _connection.Host, WebUtility.UrlEncode(name));
            return _connection.Get<Service>(address);
        }

        public Task<Nodes[]> Nodes()
        {
            var address = string.Format("{0}/catalog/nodes", _connection.Host);
            return _connection.Get<Nodes[]>(address);
        }

        public Task<Nodes[]> Nodes(string dataCenter)
        {
            var address = string.Format("{0}/catalog/nodes?dc={2}", _connection.Host, WebUtility.UrlEncode(dataCenter));
            return _connection.Get<Nodes[]>(address);
        }

        public Task<Node> Node(string name)
        {
            var address = string.Format("{0}/catalog/node/{2}", _connection.Host, WebUtility.UrlEncode(name));
            return _connection.Get<Node>(address);
        }
    }
}
