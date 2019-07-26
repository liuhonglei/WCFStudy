using Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Xml;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {

            using (ChannelFactory<IMetadataProvisionService> ChannelFactory = new ChannelFactory<IMetadataProvisionService>("mex"))
            {
                IMetadataProvisionService proxy = ChannelFactory.CreateChannel();
                string action = "http://schemas.xmlsoap.org/ws/2004/09/transfer/Get";
                Message request = Message.CreateMessage(MessageVersion.Default,action);
                Message reply = proxy.Get(request);
                MetadataSet metadata = reply.GetBody<MetadataSet>();
                using (XmlWriter writer = new XmlTextWriter("metadata.xml", Encoding.UTF8))
                {

                    metadata.WriteTo(writer);
                }

                Console.Read();
            }
        }
    }
}
