using Interface901;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Client901
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>("WSHttpBinding_CalculatorService"))
            {
                ICalculator proxy = channelFactory.CreateChannel();
                proxy.Add(1, 2);

                proxy.Add(1, 2);
            }
            Console.ReadLine();
        }
    }
}
