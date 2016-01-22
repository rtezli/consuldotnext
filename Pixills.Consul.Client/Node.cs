using Newtonsoft.Json;

namespace Pixills.Consul.Client
{
    public class Node
    {
        [JsonProperty("Node")]
        public string NodeName{get;set;}

        [JsonProperty("Services")]
        public Service[] Services{get;set;}
    }
}
