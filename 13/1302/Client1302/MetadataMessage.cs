using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;

namespace Client1302
{
    [MessageContract(IsWrapped = false)]
    public class MetadataMessage
    {
        public MetadataMessage(MetadataSet metadataSet)
        {
            Metadata = metadataSet;
        }
        [MessageBodyMember(Name = "Metadata",Namespace = "http://schemas.xmlsoap.org/ws/2004/09/mex")]
        public MetadataSet Metadata { get; set; }
    }
}
