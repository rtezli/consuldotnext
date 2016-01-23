namespace Pixills.Consul.Client
{
    public class Client
    {
        private readonly IHttpClient _httpClient;
        private readonly Catalog _catalog;
        private readonly string _datacenter;
        private readonly string _serviceName;

        public Client(IHttpClient httpClient, string datacenter, string serviceName)
        {
            _httpClient = httpClient;
            _catalog = new Catalog("", "", httpClient,  datacenter);
            _datacenter = datacenter;
            _serviceName = serviceName;
        }

        public Service[] GetServices(string name)
        {
            return null;
        }

        public void Register(string ip, string port)
        {

        }

        public void DeRegister()
        {

        }
    }
}
