using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Consul
{
  public class Catalog
  {
    public Task Register(dynamic node)
    {
      return new Task(() => {});
    }

    public void RegisterSync(dynamic node)
    {

    }

    public Task Deregister(Catalog node)
    {
      return new Task(() => {});
    }

    public void DeregisterSync(Catalog node)
    {

    }

    public Task<IEnumerable<Datacenter>> Datacenters()
    {
      return new Task<IEnumerable<Datacenter>>(() => {return Enumerable.Empty<Datacenter>();});
    }

    public IEnumerable<Datacenter> DatacentersSync()
    {
      return Enumerable.Empty<Datacenter>();
    }

    public Task<Node> Node(string nodeName)
    {
      return new Task<Node>(() => {return null;});
    }

    public Node NodeSync(string nodeName)
    {
      return null;
    }

    public Task<IEnumerable<Node>> Nodes()
    {
      return new Task<IEnumerable<Node>>(() => {return Enumerable.Empty<Node>();});
    }

    public IEnumerable<Node> NodesSync()
    {
      return null;
    }

    public Task<Service> Service(string serviceName)
    {
      return new Task<Service>(() => {return null;});
    }

    public Service ServiceSync(string serviceName)
    {
      return null;
    }

    public Task<IEnumerable<Service>> Services()
    {
      return new Task<IEnumerable<Service>>(() => {return Enumerable.Empty<Service>();});
    }

    public IEnumerable<Service> ServicesSync()
    {
      return Enumerable.Empty<Service>();
    }
  }
}
