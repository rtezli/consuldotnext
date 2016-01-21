using Newtonsoft.Json;

namespace Pixills.Consul.Client
{
  public class Service
  {
    [JsonProperty("ID")]
    public string Id{get;set;}

    [JsonProperty("Service")]
    public string ServiceName{get;set;}

    [JsonProperty("Tags")]
    public string[] Tags{get;set;}

    [JsonProperty("Address")]
    public string Address{get;set;}

    [JsonProperty("Port")]
    public ushort Port{get;set;}
  }
}
