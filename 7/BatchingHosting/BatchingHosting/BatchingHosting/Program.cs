using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Artech.BatchingHosting;

namespace BatchingHosting
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHostCollection hosts = new ServiceHostCollection())
            {
                foreach (ServiceHost host in hosts)
                {
                    host.Opened += (sender, arg) => Console.WriteLine("服务{0}开始监听", (sender as ServiceHost).Description.ServiceType);
                }
                hosts.Open();
                Console.Read();
            }
        }
    }
}
