using Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;

namespace Client1302
{

    public class MetadataProvisionService : IMetadataProvisionService
    {
        public MetadataSet Metadata { get; private set; }
        public MetadataProvisionService(MetadataSet metadata) {
            Metadata = metadata;
        }

        public Message Get(Message request)
        {
            MetadataMessage metadataMessage = new MetadataMessage(this.Metadata);
            string action = "http://schemas.xmlsoap.org/ws/2004/09/transfer/GetResponse";
            TypedMessageConverter converter = TypedMessageConverter.Create(typeof(MetadataMessage),action);
            return converter.ToMessage(metadataMessage,request.Version);
        }
    }
}
