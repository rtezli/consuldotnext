using Newtonsoft.Json;

namespace Consul
{
  public class Node
  {
    [JsonProperty("Node")]
    public string Name{get;set;}

    [JsonProperty("Address")]
    public string Address{get;set;}
  }
}
