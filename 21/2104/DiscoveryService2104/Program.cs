using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DiscoveryService2104
{
    class Program
    {
        static void Main(string[] args)
        {

            using (ServiceHost discoveryhost = new ServiceHost(typeof(DiscoveryProxyService)))
            {
                discoveryhost.Open();
                Console.Read();
            }
        }
    }
}
