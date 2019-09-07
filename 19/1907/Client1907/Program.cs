using Interface1907;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client1907
{
    class Program
    {
        static void Main(string[] args)
        {
            ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>("calculatorservice");
            NetworkCredential credential = channelFactory.Credentials.Windows.ClientCredential;
            credential.UserName = "Foo";
            credential.Password = "Password";
            ICalculator calculator = channelFactory.CreateChannel();
            Invoke(proxy => proxy.Add(1, 2), calculator, "Add");

            channelFactory = new ChannelFactory<ICalculator>("calculatorservice");
            credential = channelFactory.Credentials.Windows.ClientCredential;
            credential.UserName = "Bar";
            credential.Password = "InvalidPassword";
            calculator = channelFactory.CreateChannel();
            Invoke(proxy => proxy.Add(1, 2), calculator, "Add");
           

            Console.Read();
        }

        static void Invoke(Action<ICalculator> action, ICalculator proxy, string operation)
        {
            try
            {
                action(proxy);
                Console.WriteLine($"{operation} ok");
            }
            catch (Exception)
            {
                Console.WriteLine($"{operation} exception");
            }
        }
    }
}
