using Interface1703;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Server1703
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @".\private$\XactQueue4Demo";
            if (MessageQueue.Exists(path)) {
                MessageQueue.Delete(path);
                MessageQueue.Create(path, true);
            }
            
            using ( ServiceHost host = new ServiceHost(typeof(GreetingService)) ) {
                host.Open();
                Console.Read();
            }
        }
    }
    //[ServiceBehavior(TransactionAutoCompleteOnSessionClose = true)]
    public class GreetingService : IGreeting
    {
        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = false)]
        //[OperationBehavior(TransactionScopeRequired = true)]
        public void SayGoodBye(string name)
        {
            Console.WriteLine($"GoodBye {name}");
        }
        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = false)]
        //[OperationBehavior(TransactionScopeRequired = true)]
        public void SayHello(string name)
        {
            MsmqMessageProperty msmqMessageProperty = OperationContext.Current.GetMsmqMessageProperty();
            Console.WriteLine($"AbortCount : {msmqMessageProperty.AbortCount}");
            Console.WriteLine($"MoveCount : {msmqMessageProperty.MoveCount}");

            Console.WriteLine($"Hello {name}");
        }
    }
}
