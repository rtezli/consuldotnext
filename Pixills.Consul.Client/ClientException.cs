using System;

namespace Pixills.Consul.Client
{
    public class ClientException : Exception
    {
        public ClientException(string message)
            :base(message){}
    }
}
