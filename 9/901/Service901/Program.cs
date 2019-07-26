using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace Service901
{
    class Program
    {
        static Timer timer = new Timer(state => GC.Collect(),null,0,10);
        static void Main(string[] args)
        {
            
            using (ServiceHost host = new ServiceHost(typeof(CalculatorService)))
            {
                host.Opened += delegate
                {
                    Console.WriteLine("CalculaorService have started, press any key to stop it!");
                };

                host.Open();
                Console.Read();
            }
        }
    }
}
