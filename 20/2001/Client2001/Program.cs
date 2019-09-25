using Service.Interface2001;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client2001
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ChannelFactory<IResourceService> channelFactory = new ChannelFactory<IResourceService>("resourcesService")) {
                IResourceService proxy = channelFactory.CreateChannel();
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
                Console.WriteLine(proxy.GetString("HappyNewYear"));
                Console.WriteLine(proxy.GetString("MerryChrismas"));

                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");
                Console.WriteLine(proxy.GetString("HappyNewYear"));
                Console.WriteLine(proxy.GetString("MerryChrismas"));
            }
            Console.Read();
        }
    }
}
