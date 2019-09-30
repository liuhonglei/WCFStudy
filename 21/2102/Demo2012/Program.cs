using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Discovery;
using System.Text;
using System.Threading.Tasks;

namespace Demo2012
{
    class Program
    {
        static void Main(string[] args)
        {

            DiscoveryEndpoint dsed1, dsed2;

            dsed1 = new DiscoveryEndpoint(DiscoveryVersion.WSDiscoveryApril2005, ServiceDiscoveryMode.Adhoc);
            dsed2 = new DiscoveryEndpoint(DiscoveryVersion.WSDiscoveryApril2005, ServiceDiscoveryMode.Managed);
            Console.WriteLine($"WSDiscoveryApril2005 dsed1 {dsed1.Contract.ContractType.Name } dsed2 {dsed2.Contract.ContractType.Name }");

            dsed1 = new DiscoveryEndpoint(DiscoveryVersion.WSDiscovery11, ServiceDiscoveryMode.Adhoc);
            dsed2 = new DiscoveryEndpoint(DiscoveryVersion.WSDiscovery11, ServiceDiscoveryMode.Managed);
            Console.WriteLine($"WSDiscoveryApril2005 dsed1 {dsed1.Contract.ContractType.Name } dsed2 {dsed2.Contract.ContractType.Name }");

            dsed1 = new DiscoveryEndpoint(DiscoveryVersion.WSDiscoveryCD1, ServiceDiscoveryMode.Adhoc);
            dsed2 = new DiscoveryEndpoint(DiscoveryVersion.WSDiscoveryCD1, ServiceDiscoveryMode.Managed);
            Console.WriteLine($"WSDiscoveryApril2005 dsed1 {dsed1.Contract.ContractType.Name } dsed2 {dsed2.Contract.ContractType.Name }");

            AnnouncementEndpoint announcementEndpoint;
            announcementEndpoint = new AnnouncementEndpoint(DiscoveryVersion.WSDiscovery11);
            Console.WriteLine($"WSDiscovery11 dsed1 {announcementEndpoint.Contract.ContractType.Name } ");

            announcementEndpoint = new AnnouncementEndpoint(DiscoveryVersion.WSDiscoveryApril2005);
            Console.WriteLine($"WSDiscoveryApril2005 dsed1 {announcementEndpoint.Contract.ContractType.Name } ");

            announcementEndpoint = new AnnouncementEndpoint(DiscoveryVersion.WSDiscoveryCD1);
            Console.WriteLine($"WSDiscoveryCD1 dsed1 {announcementEndpoint.Contract.ContractType.Name } ");
            Console.Read();
        }
    }
}
