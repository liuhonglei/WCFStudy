using Service;
using Service.Inteface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            //InstanceContext callback = new InstanceContext(new CalculatorCallbackService());
            //using (DuplexChannelFactory<ICalculator> channelFactory = new DuplexChannelFactory<ICalculator>(callback, "calculatorservice"))
            //{
            //    ICalculator calculator = channelFactory.CreateChannel();
            //    calculator.Add(1, 2);
            //}
            using (ServiceHost host = new ServiceHost(typeof(CalculatorService)))
            {
                host.Open();
                Console.WriteLine("start..");
                Console.Read();
            }
        }
    }
}
