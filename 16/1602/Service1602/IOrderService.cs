using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Service1602
{
    [ServiceContract]
    [DeliveryRequirements(  RequireOrderedDelivery = true)]
     interface IOrderService
    {
        [OperationContract]
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
