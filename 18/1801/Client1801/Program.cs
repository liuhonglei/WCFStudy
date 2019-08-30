using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client1801
{
    class Program
    {
        static void Main(string[] args)
        {
            //服务端 必须使用https 地址
            var binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;

            //客户端
            var binding1 = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            binding1.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
            ChannelFactory<ICalcultor>    channelFactory = new ChannelFactory<ICalcultor> (binding,"https://www.lhl.com/calculatorservice")


            BasicHttpMessageSecurity



        }
    }

}