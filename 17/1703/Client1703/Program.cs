using Interface1703;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Client1703
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ChannelFactory<IGreeting> ChannelFactory = new ChannelFactory<IGreeting>("greetingService"))
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    IGreeting proxy = ChannelFactory.CreateChannel();
                    
                    proxy.SayHello("Foo");
                    proxy.SayGoodBye("Bar");
                    (proxy as ICommunicationObject).Close();
                    scope.Complete();
                }

            }
            Console.Read();


        }
    }
}
