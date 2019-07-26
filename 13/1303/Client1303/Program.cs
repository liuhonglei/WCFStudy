using Interface1303;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Xml;

namespace Client1303
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test1();

            Binding binding = MetadataExchangeBindings.CreateMexHttpBinding();
            EndpointAddress address = new EndpointAddress("http://127.0.0.1:9999/calculatorservice/mex");
            MetadataExchangeClient client = new MetadataExchangeClient(binding);
            MetadataSet metadata = client.GetMetadata(address);
            WsdlImporter importer = new WsdlImporter(metadata);

            ContractDescription contract = ContractDescription.GetContract(typeof(ICalculator1));

            importer.KnownContracts.Add(new XmlQualifiedName(contract.Name,contract.Namespace), contract);
            ServiceEndpointCollection serviceEndpoints = importer.ImportAllEndpoints();
            using (ChannelFactory<ICalculator1> channelFactory = new ChannelFactory<ICalculator1>(serviceEndpoints[0]))
            {
                ICalculator1 calculator1 = channelFactory.CreateChannel();
                Console.WriteLine($"result : { calculator1.Add(1, 2) }");  

            }

                Console.Read();
        }

        private static void Test1()
        {
            Uri address = new Uri("http://127.0.0.1:9999/calculatorservice/metadata");
            MetadataExchangeClient client = new MetadataExchangeClient(address, MetadataExchangeClientMode.HttpGet);
            client.ResolveMetadataReferences = false;
            MetadataSet metadata = client.GetMetadata();
            using (XmlWriter writer = new XmlTextWriter("metadata.xml", Encoding.UTF8))
            {
                metadata.WriteTo(writer);
            }

            WsdlImporter importer = new WsdlImporter(metadata);
            importer.ImportAllEndpoints();
            importer.ImportAllContracts();
        }
    }
}
