using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pixills.Consul.Client
{
    public class Catalog
    {
        private readonly IHttpClient _client;
        private readonly string _serviceBaseUrl;
        private readonly string _accessToken;
        private readonly string _dataCenter;
        private const string _apiVersion = "v1";

        public Catalog(string serviceBaseUrl, string accessToken, IHttpClient client, string dataCenter)
        {
            _client = client;
            _accessToken = accessToken;
            _serviceBaseUrl = serviceBaseUrl;
            _dataCenter = dataCenter;
        }

        public Task<string[]> Datacenters()
        {
            var address = string.Format("{0}/{1}/catalog/datacenters",_serviceBaseUrl, _apiVersion);
            return _client.Get<string[]>(address);
        }

        public Task<Dictionary<string, string[]>> Services()
        {
            var address = string.Format("{0}/{1}/catalog/services",_serviceBaseUrl, _apiVersion);
            return _client.Get<Dictionary<string, string[]>>(address);
        }

        public Task<Dictionary<string, string[]>> Services(string dataCenter)
        {
            var address = string.Format("{0}/{1}/catalog/services?dc={2}",_serviceBaseUrl, _apiVersion, WebUtility.UrlEncode(dataCenter));
            return _client.Get<Dictionary<string, string[]>>(address);
        }

        public Task<Service> Service(string name)
        {
            var address = string.Format("{0}/{1}/catalog/service/{2}",_serviceBaseUrl, _apiVersion, WebUtility.UrlEncode(name));
            return _client.Get<Service>(address);
        }

        public Task<Nodes[]> Nodes()
        {
            var address = string.Format("{0}/{1}/catalog/nodes",_serviceBaseUrl, _apiVersion);
            return _client.Get<Nodes[]>(address);
        }

        public Task<Nodes[]> Nodes(string dataCenter)
        {
            var address = string.Format("{0}/{1}/catalog/nodes?dc={2}",_serviceBaseUrl, _apiVersion, WebUtility.UrlEncode(dataCenter));
            return _client.Get<Nodes[]>(address);
        }

        public Task<Node> Node(string name)
        {
            var address = string.Format("{0}/{1}/catalog/node/{2}",_serviceBaseUrl, _apiVersion, WebUtility.UrlEncode(name));
            return _client.Get<Node>(address);
        }
    }
}
