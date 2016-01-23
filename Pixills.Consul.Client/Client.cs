using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Pixills.Consul.Client
{
    public class Client
    {
        private readonly IHttpClient _httpClient;
        private readonly string _datacenter;
        private readonly string _serviceName;
        private readonly string _serviceBaseUrl;
        //private readonly string _accessToken;
        private const string _apiVersion = "v1";

        public Client(string consulHostUrl, IHttpClient httpClient, string datacenter, string serviceName)
        {
            _httpClient = httpClient;
            _datacenter = datacenter;
            _serviceName = serviceName;
            _serviceBaseUrl = consulHostUrl;
        }

        public Task<string[]> Datacenters()
        {
            var address = string.Format("{0}/{1}/catalog/datacenters", _serviceBaseUrl, _apiVersion);
            return _httpClient.Get<string[]>(address);
        }

        public Task<Dictionary<string, string[]>> Services()
        {
            var address = string.Format("{0}/{1}/catalog/services",_serviceBaseUrl, _apiVersion);
            return _httpClient.Get<Dictionary<string, string[]>>(address);
        }

        public Task<Dictionary<string, string[]>> Services(string dataCenter)
        {
            var address = string.Format("{0}/{1}/catalog/services?dc={2}",_serviceBaseUrl, _apiVersion, WebUtility.UrlEncode(dataCenter));
            return _httpClient.Get<Dictionary<string, string[]>>(address);
        }

        public Task<Service> Service(string name)
        {
            var address = string.Format("{0}/{1}/catalog/service/{2}",_serviceBaseUrl, _apiVersion, WebUtility.UrlEncode(name));
            return _httpClient.Get<Service>(address);
        }

        public Task<Nodes[]> Nodes()
        {
            var address = string.Format("{0}/{1}/catalog/nodes",_serviceBaseUrl, _apiVersion);
            return _httpClient.Get<Nodes[]>(address);
        }

        public Task<Nodes[]> Nodes(string dataCenter)
        {
            var address = string.Format("{0}/{1}/catalog/nodes?dc={2}",_serviceBaseUrl, _apiVersion, WebUtility.UrlEncode(dataCenter));
            return _httpClient.Get<Nodes[]>(address);
        }

        public Task<Node> Node(string name)
        {
            var address = string.Format("{0}/{1}/catalog/node/{2}",_serviceBaseUrl, _apiVersion, WebUtility.UrlEncode(name));
            return _httpClient.Get<Node>(address);
        }
    }
}
