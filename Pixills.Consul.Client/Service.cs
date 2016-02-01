using Newtonsoft.Json;

namespace Pixills.Consul.Client
{
    public class Service
    {
        [JsonProperty("ServiceID")]
        public string Id { get; set; }

        [JsonProperty("ServiceName")]
        public string Name { get; set; }

        [JsonProperty("ServiceTags")]
        public string[] Tags { get; set; }

        [JsonProperty("Node")]
        public string Node { get; set; }

        [JsonProperty("ServiceAddress")]
        public string Address { get; set; }

        [JsonProperty("ServicePort")]
        public ushort Port { get; set; }
    }
}
