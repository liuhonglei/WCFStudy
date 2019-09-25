using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Service2001
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(ResourceService)))
            {
                //host.Opened += delegate
                //{
                //    Console.WriteLine("CalculaorService have started, press any key to stop it!");
                //};

               
                host.Opened += Host_Opened;
                host.Open();
                Console.Read();
            }

        }

        private static void Host_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("open success");
        }
    }
}
