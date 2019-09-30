using Service.Interface2102;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using System.Text;
using System.Threading.Tasks;

namespace Client2102
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 两个步骤服务发现
            //DiscoveryClient discoveryClient = new DiscoveryClient(new UdpDiscoveryEndpoint());
            //FindCriteria findCriteria = new FindCriteria(typeof(ICalculator));
            //findCriteria.Scopes.Add(new Uri("http://www.lhl.com"));
            //FindResponse response = discoveryClient.Find(findCriteria);
            //if (response.Endpoints.Count > 0)
            //{
            //    EndpointAddress endpointAddress = response.Endpoints[0].Address;
            //    using (ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>(new WS2007HttpBinding(), endpointAddress))
            //    {
            //        var channel = channelFactory.CreateChannel();
            //        Console.WriteLine(channel.Add(2, 3));
            //    }
                
            //}
            #endregion

            #region DynamicEndpoint
            using (ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>("calculatorservice"))
            {
                var channel = channelFactory.CreateChannel();
                Console.WriteLine(channel.Add(2, 3));
            }
            #endregion
            Console.Read();

        }
    }
}
