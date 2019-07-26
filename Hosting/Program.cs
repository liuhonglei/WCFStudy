using Service;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Hosting
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 代码中直接寄宿
            //using (ServiceHost host = new ServiceHost(typeof(CalculatorService)))
            //{
            //    host.AddServiceEndpoint(typeof(ICalculator), new WSHttpBinding(), "http://127.0.0.1:9999/calculatorservice");
            //    if (host.Description.Behaviors.Find<ServiceMetadataBehavior>() == null)
            //    {
            //        ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
            //        behavior.HttpGetEnabled = true;
            //        behavior.HttpGetUrl = new Uri("http://127.0.0.1:9999/calculatorservice/metadata");
            //        host.Description.Behaviors.Add(behavior);
            //    }
            //    host.Opened += delegate
            //    {
            //        Console.WriteLine("CalculaorService已经启动，按任意键终止服务！");
            //    };

            //    host.Open();
            //    Console.Read();
            //}
            #endregion

            #region  app.config配置 进行寄宿
            using (ServiceHost host = new ServiceHost(typeof(CalculatorService)))
            {
                host.Opened += delegate
                {
                    Console.WriteLine("CalculaorService have started, press any key to stop it!");
                };

                host.Open();
                Console.Read();
            }

            #endregion
        }
    }
}
