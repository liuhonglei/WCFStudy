using Interface1906;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client1906
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
            Invoke(proxy => proxy.Subtract(1, 2), calculator, "Subtract");
            Invoke(proxy => proxy.Divide(1, 2), calculator, "Divide");
            Invoke(proxy => proxy.Multiply(1, 2), calculator, "Multiply");

            Console.WriteLine();

            channelFactory = new ChannelFactory<ICalculator>("calculatorservice");
             credential = channelFactory.Credentials.Windows.ClientCredential;
            credential.UserName = "Bar";
            credential.Password = "Password";
             calculator = channelFactory.CreateChannel();
            Invoke(proxy => proxy.Add(1, 2), calculator, "Add");
            Invoke(proxy => proxy.Subtract(1, 2), calculator, "Subtract");
            Invoke(proxy => proxy.Divide(1, 2), calculator, "Divide");
            Invoke(proxy => proxy.Multiply(1, 2), calculator, "Multiply");

            Console.Read();

        }
        static void Invoke(Action<ICalculator>  action , ICalculator proxy,string operation)
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
