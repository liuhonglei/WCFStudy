using Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.ServiceModel;
using System.Text;

namespace Client_801
{
    class Program
    {
        static void Main(string[] args)
        {

            using (ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>("WSHttpBinding_CalculatorService"))
            {
                //RealProxy

                ICalculator proxy = channelFactory.CreateChannel();
                ((ICommunicationObject)proxy).Open();

                IContextChannel contextChannel = proxy as IContextChannel;
                Console.WriteLine("{0,-21}:{1}","LocalAddress",contextChannel.LocalAddress);
                Console.WriteLine("{0,-21}:{1}", "RemoteAddress", contextChannel.RemoteAddress);
                Console.WriteLine("{0,-21}:{1}", "InputSession", contextChannel.InputSession == null);
                Console.WriteLine("{0,-21}:{1}", "InputSession", contextChannel.InputSession == null);
                Console.WriteLine("{0,-21}:{1}", "SessionId", contextChannel.SessionId);
                Console.WriteLine("{0,-21}:{1}", "AllowOutputBatching", contextChannel.AllowOutputBatching);
                Console.WriteLine("{0,-21}:{1}", "OperationTimeout", contextChannel.OperationTimeout);

                Console.WriteLine("x + y = {2} when x ={0} and y = {1}", 1, 2, proxy.Add(1, 2));
            }

            Console.ReadLine();
        }
    }
}
