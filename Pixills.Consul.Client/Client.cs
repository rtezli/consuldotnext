using Pixills.Net.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pixills.Consul.Client
{
    public class Client
    {
        private Catalog _catalog;
        private readonly string _nodeName;
        private readonly string _serviceName;
        private readonly string _datacenterName;
        public Dictionary<string, string> Services { get; }

        public Client()
        {
            _nodeName = Environment.GetEnvironmentVariable("CONSUL_CLIENT_NODE_NAME");
            _serviceName = Environment.GetEnvironmentVariable("CONSUL_CLIENT_SERVICE_NAME");
            _datacenterName = Environment.GetEnvironmentVariable("CONSUL_CLIENT_DATACENTER_NAME");
            var connection = new HttpConnection("localhost:8500", new HttpClient(), false, 5);
            _catalog = new Catalog(connection);
        }

        public Task<Dictionary<string, string[]>> GetServices()
        {
            return _catalog.Services();
        }

        public Task<Service> GetService(string name)
        {
            return _catalog.Service(name);
        }

        public Task Register(Service service)
        {
            return _catalog.Register(new
            {
                DataCenter = _datacenterName,
                Service = _serviceName,
                Node = _nodeName
            });
        }
    }
}
