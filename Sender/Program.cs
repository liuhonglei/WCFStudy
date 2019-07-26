using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri listenUri = new Uri("http://127.0.0.1:3721/listener");
            Binding binding = new BasicHttpBinding();
            
            IChannelFactory<IRequestChannel> channelFactory = binding.BuildChannelFactory<IRequestChannel>();
            channelFactory.Open();

            IRequestChannel channel = channelFactory.CreateChannel(new EndpointAddress(listenUri));
            channel.Open();
            
            Message replyMessage = channel.Request(CreateRequestMessage(binding));
            Console.WriteLine(replyMessage);
            Console.Read();
        }

        private static Message CreateRequestMessage(Binding binding)
        {
            string action = "http://www.lhl.com/calculatorservice/Add";
            XNamespace ns = "http://www.lhl.com";
            XElement body = new XElement(new XElement(ns + "Add", new XElement(ns + "x", 1), new XElement(ns + "y", 2)));
            return Message.CreateMessage(binding.MessageVersion, action, body);
        }
    }
}
