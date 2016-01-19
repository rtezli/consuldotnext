using System.Threading;

namespace Consul
{
  public class Catalog
  {

    public async Task Register(Node node)
    {

    }

    public void RegisterSync(Node node)
    {

    }

    public async Task Register(Service service)
    {

    }

    public void RegisterSync(Service service)
    {

    }

    public async Task Register(Check check)
    {

    }

    public void RegisterSync(Check check)
    {

    }

    public async Task Deregister(Node node)
    {

    }

    public void DeregisterSync(Node node)
    {

    }

    public async Task Deregister(Service service)
    {

    }

    public void DeregisterSync(Service service)
    {

    }

    public async Task Deregister(Check check)
    {

    }

    public void DeregisterSync(Check check)
    {

    }

    public async Task<IEnumerable<Datacenter>> Datacenters()
    {

    }

    public IEnumerable<Datacenter> DatacentersSync()
    {

    }

    public async Task<Nodes> Node(string nodeName)
    {

    }

    public Node NodeSync(string nodeName)
    {

    }

    public async Task<IEnumerable<Node>> Nodes()
    {

    }

    public IEnumerable<Node> NodesSync()
    {

    }

    public async Task<Service> Service(string serviceName)
    {

    }

    public Service ServiceSync(string serviceName)
    {

    }

    public async Task<IEnumerable<Service>> Services()
    {

    }

    public IEnumerable<Service> ServicesSync()
    {

    }

  }
}
