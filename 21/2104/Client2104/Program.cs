using Service.Interface2104;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client2104
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>("calculatorservice"))
            {
                var channel = channelFactory.CreateChannel();
                Console.WriteLine(channel.Add(2, 3));
                Console.Read();
            }


        }
    }
}
