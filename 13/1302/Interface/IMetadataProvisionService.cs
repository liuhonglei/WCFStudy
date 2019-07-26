using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace Interface
{
    [ServiceContract(ConfigurationName = "IMetadataProvisionService",
       Name = "IMetadataProvisionService", Namespace = "http://schemas.microsoft.com/2006/04/mex")]
    public interface IMetadataProvisionService
    {
        [OperationContract(Action = "http://schemas.xmlsoap.org/ws/2004/09/transfer/Get",
            ReplyAction = "http://schemas.xmlsoap.org/ws/2004/09/transfer/GetResponse")]
        Message Get(Message request);

    }

    [ServiceContract(Namespace = "http://www.lhl.com")]
   public interface ICalculator1
    {
        [OperationContract]
        double Add(double x, double y);
        [OperationContract]
        double Substract(double x, double y);
    }
}
