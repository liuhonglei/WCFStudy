using Client.ServiceReference1;
//using Client.ServiceReference1;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Routing;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    
    class Program
    {
        static void Main(string[] args)
        {
           RoutingConfiguration
            #region 使用客户端自己生成的引用 调用服务
            using (CalculatorServiceClient proxy = new CalculatorServiceClient()) {
                Console.WriteLine("x + y = {2} when x ={0} and y = {1}",1,2,proxy.Add(1,2));    
            }
            #endregion 
            #region 使用ChannelFactory 调用服务
            using (ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>(new WSHttpBinding(), "http://127.0.0.1:9999/calculatorservice"))
            {
                ICalculator proxy = channelFactory.CreateChannel();
                Console.WriteLine("x + y = {2} when x ={0} and y = {1}", 1, 2, proxy.Add(1, 2));
            }
            #endregion
            
            #region 使用channelfactory 并通过配置 指定终结点名称  "calculatorservice"
            using (ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>("calculatorservice"))
            {
                ICalculator proxy = channelFactory.CreateChannel();
                Console.WriteLine("x + y = {2} when x ={0} and y = {1}", 1, 2, proxy.Add(1, 2));
            }
            #endregion
        }
    }
}
