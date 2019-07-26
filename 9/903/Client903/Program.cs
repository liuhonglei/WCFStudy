using Interface903;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Client903
{
    class Program
    {
        static void Main(string[] args)
        {
                using (ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>("WSHttpBinding_CalculatorService"))
                {
                    ICalculator proxy = channelFactory.CreateChannel();
                    proxy.Add(1, 2);
                    //ICalculator proxy2 = channelFactory.CreateChannel();
                    //proxy2.Add(1, 2);
                }
                Console.ReadLine();
        }
    }
}
