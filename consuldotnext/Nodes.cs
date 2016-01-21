using Newtonsoft.Json;

namespace Consul
{
  public class Nodes
  {
    [JsonProperty("Node")]
    public string NodeName{get;set;}

    [JsonProperty("Address")]
    public string Address{get;set;}
  }
}
