using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Client1801
{
    class Program
    {
        static void Main(string[] args)
        {
            ////服务端 必须使用https 地址
            //var binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            //binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;

            ////客户端
            //var binding1 = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            //binding1.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
            //ChannelFactory<ICalcultor> channelFactory = new ChannelFactory<ICalcultor>(binding, "https://www.lhl.com/calculatorservice");

            var binding2 = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            binding2.PrintProtectionLevel("Transport");


            binding2 = new BasicHttpBinding(BasicHttpSecurityMode.Message);
            binding2.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.Certificate;
            binding2.PrintProtectionLevel("Message");


            binding2 = new BasicHttpBinding(BasicHttpSecurityMode.TransportWithMessageCredential);
            binding2.PrintProtectionLevel("Mixed");

            binding2 = new BasicHttpBinding(BasicHttpSecurityMode.TransportCredentialOnly);
            binding2.PrintProtectionLevel("TransportCredentialOnly");

            Console.ReadLine();
        }
    }

    public static class BindingExtension
    {
        public static void PrintProtectionLevel(this Binding binding,string securityMode) {
            var bindingParameters = new BindingParameterCollection();
            var requestProtectionLevel = binding.GetProperty<ISecurityCapabilities>(bindingParameters).SupportedRequestProtectionLevel;
            var responseProtectionLevel = binding.GetProperty<ISecurityCapabilities>(bindingParameters).SupportedResponseProtectionLevel;
            Console.WriteLine($"{securityMode}  {requestProtectionLevel }  {responseProtectionLevel}");

        }

    }

}