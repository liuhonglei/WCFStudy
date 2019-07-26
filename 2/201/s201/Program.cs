using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Service.Interface;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace s201
{
    class Program
    {
        static void Main(string[] args)
        {
            
            using (ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>("calculatorservice"))
            {
                ClientRuntime
                ICalculator proxy = channelFactory.CreateChannel();
                using (OperationContextScope scope = new OperationContextScope(proxy as IClientChannel))
                {
                    //string sn = "{5a259f00-172f-4341-a4ab-d02e5fba8ecc}";
                    //string ns = "http://www.lhl.com/";
                    //AddressHeader header = AddressHeader.CreateAddressHeader("sn", ns, sn);
                    //MessageHeader messageHeader = header.ToMessageHeader();
                    //OperationContext.Current.OutgoingMessageHeaders.Add(messageHeader);
                    Console.WriteLine("x + y = {2} when x = {0} and y = {1}", 1, 2, proxy.Add(1, 2));
                    
                } 
            }
            Console.Read();
        }
    }
}
