using Newtonsoft.Json;

namespace Pixills.Consul.Client
{
    public class Serializer
    {
        public static string Serialize(object o)
        {
            return JsonConvert.SerializeObject(o,
                Formatting.None,
                new JsonSerializerSettings
                {
                  NullValueHandling = NullValueHandling.Ignore
                });
            }
    }
}
