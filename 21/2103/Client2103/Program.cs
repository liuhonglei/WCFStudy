using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using System.Text;
using System.Threading.Tasks;

namespace Client2103
{
    class Program
    {
        static void Main(string[] args)
        {

            AnnouncementService announcementService = new AnnouncementService();
            announcementService.OnlineAnnouncementReceived += AnnouncementService_OnlineAnnouncementReceived;
            announcementService.OfflineAnnouncementReceived += AnnouncementService_OfflineAnnouncementReceived;
            using (ServiceHost host = new ServiceHost(announcementService))
            {
                host.Open();
                Console.Read();
            }

        }

        private static void AnnouncementService_OfflineAnnouncementReceived(object sender, AnnouncementEventArgs e)
        {
            Console.WriteLine("offline");
            Console.WriteLine(e.EndpointDiscoveryMetadata.Address.Uri);
            Console.WriteLine(e.EndpointDiscoveryMetadata.ContractTypeNames[0].Name);
        }

        private static void AnnouncementService_OnlineAnnouncementReceived(object sender, AnnouncementEventArgs e)
        {
            Console.WriteLine("online");
            Console.WriteLine( e.EndpointDiscoveryMetadata.Address.Uri);
            Console.WriteLine(e.EndpointDiscoveryMetadata.ContractTypeNames[0].Name);
        }


    }
}
