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

        public Task<Dictionary<string, string[]>> Services()
        {
            return _connection.Get<Dictionary<string, string[]>>("/catalog/services");
        }

        public Task<Dictionary<string, string[]>> Services(string dataCenter = null)
        {
            var address = $"/catalog/services{(dataCenter == null ? "" : WebUtility.UrlEncode(dataCenter))}";
            return _connection.Get<Dictionary<string, string[]>>(address);
        }

        internal Task Register(object o)
        {
            return _connection.Put("/catalog/register", o);
        }

        public Task<Service> Service(string name)
        {
            var address = $"/catalog/service/{(WebUtility.UrlEncode(name))}";
            return _connection.Get<Service>(address);
        }

        public Task<Nodes[]> Nodes()
        {
            return _connection.Get<Nodes[]>("/catalog/nodes");
        }

        public Task<Nodes[]> Nodes(string dataCenter)
        {
            var address = $"/catalog/nodes{(dataCenter == null ? "" : WebUtility.UrlEncode(dataCenter))}";
            return _connection.Get<Nodes[]>(address);
        }

        public Task<Node> Node(string name)
        {
            var address = $"/catalog/node/{(WebUtility.UrlEncode(name))}";
            return _connection.Get<Node>(address);
        }
    }
}
