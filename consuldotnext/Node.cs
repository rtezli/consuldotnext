using Newtonsoft.Json;

namespace Consul
{
  public class Node
  {
    [JsonProperty("Node")]
    public string NodeName{get;set;}

    [JsonProperty("Services")]
    public Service[] Services{get;set;}
  }
}
