using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri listenUri = new Uri("https://127.0.0.1:3721/listener");
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.MessageEncoding = WSMessageEncoding.Mtom;
            binding.ListAllBindingElements();

             binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
             Console.WriteLine("Transport");
             binding.MessageEncoding = WSMessageEncoding.Mtom;
             binding.ListAllBindingElements();

             binding = new BasicHttpBinding(BasicHttpSecurityMode.Message);
             binding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.Certificate;
             Console.WriteLine("Message");
             binding.MessageEncoding = WSMessageEncoding.Mtom;
             binding.ListAllBindingElements();

             binding = new BasicHttpBinding(BasicHttpSecurityMode.TransportWithMessageCredential);
             Console.WriteLine("TransportWithMessageCredential");
             binding.MessageEncoding = WSMessageEncoding.Mtom;
             binding.ListAllBindingElements();
            
            bool flag = binding.CanBuildChannelFactory<IDuplexChannel>(listenUri);
            //创建，开启信道监听器
            IChannelListener<IReplyChannel> channelListener = binding.BuildChannelListener<IReplyChannel>(listenUri);
            channelListener.Open();
            //创建开启回复信道
            IReplyChannel channel = channelListener.AcceptChannel(TimeSpan.MaxValue);
            channel.Open();
            
                
            //开始监听
            while (true)
            {
                //接受输出请求消息
                RequestContext requestContext = channel.ReceiveRequest(TimeSpan.MaxValue);
                Console.WriteLine(requestContext.RequestMessage);
                //消息回复
                requestContext.Reply(CreateReplyMessage(binding));
            }
            
        }

        private static Message CreateReplyMessage(Binding binding)
        {
            string action = "http://www.lhl.com/calculatorservice/AddResponse";
            XNamespace ns = "http://www.lhl.com";
            XElement body = new XElement(new XElement(ns + "AddResponse", new XElement(ns + "AddResult", 3)));
            return Message.CreateMessage(binding.MessageVersion,action,body);
        }
    }
}
