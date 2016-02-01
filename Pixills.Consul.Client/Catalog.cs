using System.Net;
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
            return _connection.Get<string[]>("/catalog/datacenters");
        }

        public Task<Dictionary<string, string[]>> Services(string dataCenter = null)
        {
            var dc = string.IsNullOrWhiteSpace(dataCenter) ? "" : $"?dc={WebUtility.UrlEncode(dataCenter)}";
            var address = $"/catalog/services{dc}";
            return _connection.Get<Dictionary<string, string[]>>(address);
        }

        internal Task Register(object o)
        {
            return _connection.Put("/catalog/register", o);
        }

        public Task<Service> Service(string name)
        {
            var address = $"/catalog/service/{WebUtility.UrlEncode(name)}";
            return _connection.Get<Service>(address);
        }

        public Task<Node> Node(string name)
        {
            var address = $"/catalog/node/{(WebUtility.UrlEncode(name))}";
            return _connection.Get<Node>(address);
        }

        public Task<Nodes[]> Nodes(string dataCenter = null)
        {
            var dc = string.IsNullOrWhiteSpace(dataCenter) ? "" : $"?dc={WebUtility.UrlEncode(dataCenter)}";
            var address = $"/catalog/nodes{dc}";
            return _connection.Get<Nodes[]>(address);
        }
    }
}
