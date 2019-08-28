using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace _17
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(OrderService))) {
                //try
                //{

                host.AddServiceEndpoint(typeof(IOrderService), new NetMsmqBinding(NetMsmqSecurityMode.None ), "net.msmq://localhost/private/queuename");
                host.Open();
                Console.ReadKey();
                //}
                //catch(Exception ex)
                //{

                //}
                //finally
                //{
                //    host.Close();
                //}  
            }

        }
    }

    [ServiceContract]
    //[DeliveryRequirements(RequireOrderedDelivery = true)]
    interface IOrderService
    {
        [OperationContract(IsOneWay = true)]
        void Process();
    }

    public class OrderService : IOrderService
    {
        public void Process()
        {
            Console.WriteLine("order");
        }
    }
}
