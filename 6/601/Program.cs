using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Xml;

namespace _601
{
    class Program
    {
        static void Main(string[] args)
        {
            string action = "http://www.lhl.com/Add";
            using (Message message = Message.CreateMessage(MessageVersion.Soap12WSAddressingAugust2004, action)) {
                string ns = "http://www.lhl.com/crm";
                EndpointAddress address = new EndpointAddress("http://www.lhl.com/client");
                message.Headers.To = new Uri("http://www.lhl.com/crm/Customerservice");
                message.Headers.From = address;
                message.Headers.ReplyTo = address;
                message.Headers.FaultTo = address;
                message.Headers.MessageId = new System.Xml.UniqueId(Guid.NewGuid());
                message.Headers.RelatesTo = new System.Xml.UniqueId(Guid.NewGuid());

                //soap1.2
                string ultimateReceiver = "http://www.w3.org/2003/05/soap-envelope/role/ultimateReceiver";
                MessageHeader<string> foo = new MessageHeader<string>("ABC",false, ultimateReceiver,false);
                MessageHeader<string> bar = new MessageHeader<string>("abc", true, ultimateReceiver, false);
                MessageHeader<string> baz = new MessageHeader<string>("123", false, "http://shcemas.xmlsoap.org/soap/actor/next", true);


                ////soap 1.1
                //MessageHeader<string> foo = new MessageHeader<string>("ABC");
                //MessageHeader<string> bar = new MessageHeader<string>("abc",true,"",false);
                //MessageHeader<string> baz = new MessageHeader<string>("123",false,"http://shcemas.xmlsoap.org/soap/actor/next",true);

                message.Headers.Add(foo.GetUntypedHeader("Foo",ns) );
                message.Headers.Add(bar.GetUntypedHeader("Bar", ns));
                message.Headers.Add(baz.GetUntypedHeader("Baz", ns));

                WriteMessage(message, "message12.xml");
                //message.WriteMessage
            }

            Console.ReadLine();
        }

        static void WriteMessage(Message message, string fileName)
        {
            using (XmlWriter writer = new XmlTextWriter(fileName, Encoding.UTF8))
            {
                message.WriteMessage(writer);
            }
        }
    }
}
