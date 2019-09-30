using Service.Interface2101;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client2101
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ChannelFactory<IHello> helloFactory = new ChannelFactory<IHello>("helloService"))
            using (ChannelFactory<IGoodBye> goodbyeFactory = new ChannelFactory<IGoodBye>("goodbyeService"))
            {
                IHello hello = helloFactory.CreateChannel();
                IGoodBye goodBye = goodbyeFactory.CreateChannel();
                Console.WriteLine(hello.SayHello("z3"));
                Console.WriteLine(goodBye.SayGoodBye("w5"));
            }
            Console.Read();
        }
    }
}
