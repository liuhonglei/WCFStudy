using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.Text;
using System.Xml;

namespace Client1301
{
    class Program
    {
        static void Main(string[] args)
        {
            //MetadataSection metadataSection = new MetadataSection();
            //MetadataLocation metadataLocation = new MetadataLocation();
            //WsdlExporter wsdlExporter = new WsdlExporter();
            ContractDescription contract = ContractDescription.GetContract(typeof(IOrderService));
            EndpointAddress endpointAddress1 = new EndpointAddress("http://127.0.0.1/orderservice");
            EndpointAddress endpointAddress2 = new EndpointAddress("net.tcp://127.0.0.1/orderservice");
            ServiceEndpoint endpoint1 = new ServiceEndpoint(contract, new WSHttpBinding(), endpointAddress1);
            ServiceEndpoint endpoint2 = new ServiceEndpoint(contract, new NetTcpBinding(), endpointAddress2);
            XmlQualifiedName serviceName = new XmlQualifiedName("OrderService","http://www.lhl.com");
            WsdlExporter wsdlExporter = new WsdlExporter();
            wsdlExporter.ExportEndpoints(new ServiceEndpoint[] { endpoint1,endpoint2}, serviceName);
            MetadataSet metadata = wsdlExporter.GetGeneratedMetadata();
            using ( XmlWriter writer = new XmlTextWriter("metadata.xml",Encoding.UTF8)) {

                metadata.WriteTo(writer);
            }
            

        }
    }

    [ServiceContract(Namespace = "http:www.lhl.com")]
    public interface IOrderService {
        [OperationContract]
        void ProcessOrder(Order order);

    }

    [DataContract(Namespace = "http:www.lhl.com")]
    public class Order
    {
        [DataMember]
        public string OrderId { get; set; }
        [DataMember]
        public string CustomerId { get; set; }
        [DataMember]
        public Collection<OrderDetail> Details { get; set; }

    }
    [DataContract(Namespace = "http:www.lhl.com")]
    public class OrderDetail
    {
        [DataMember]
        public string OrderId { get; set; }
        [DataMember]
        public string ProductId { get; set; }
        [DataMember]
        public int Quantity { get; set; }
    }
}
