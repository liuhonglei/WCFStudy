using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Service2104
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (ServiceHost discoveryhost = new ServiceHost(typeof(DiscoveryProxyService)))
            using (ServiceHost calculatorService = new ServiceHost(typeof(CalculatorService)))
            {
                //discoveryhost.Open();
                calculatorService.Open();
                Console.Read();
            }
        }
    }
}
