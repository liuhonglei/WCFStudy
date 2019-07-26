using Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Xml;

namespace Client1302
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceMetadataBehaviorAttribute : Attribute, IServiceBehavior
    {
        private const string MexContractName = "IMetadataProvisionService";
        private const string MexContractNamespace = "http://schemas.microsoft.com/2006/04/mex";
        private const string SingletonInstanceContextProviderType = "System.ServiceModel.Dispatcher.SingletonInstanceContextProvider, System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            MetadataSet metadata = GetExportedMetadata(serviceDescription);
            CustomizeMexEndpoint(serviceDescription, serviceHostBase, metadata);
        }

        private MetadataSet GetExportedMetadata(ServiceDescription serviceDescription)
        {
            Collection<ServiceEndpoint> endpoints = new Collection<ServiceEndpoint>();
            foreach (var endpoint in serviceDescription.Endpoints)
            {
                if (endpoint.Contract.ContractType == typeof(IMetadataProvisionService))
                    continue;
                ServiceEndpoint newEndpoint = new ServiceEndpoint(endpoint.Contract,endpoint.Binding,endpoint.Address);
                newEndpoint.Name = endpoint.Name;
                foreach (var behavior in endpoint.Behaviors)
                {
                    newEndpoint.Behaviors.Add(behavior);
                }
                endpoints.Add(newEndpoint);
            }

            WsdlExporter wsdlExporter = new WsdlExporter();
            XmlQualifiedName wsxmlQualifiedName = new XmlQualifiedName(serviceDescription.Name, serviceDescription.Namespace);
            wsdlExporter.ExportEndpoints(endpoints, wsxmlQualifiedName);
            return wsdlExporter.GetGeneratedMetadata();

        }

        private void CustomizeMexEndpoint(ServiceDescription serviceDescription, ServiceHostBase host, MetadataSet metadata)
        {
            foreach (ChannelDispatcher channelDispatcher in host.ChannelDispatchers)
            {
                foreach (var endpoint in channelDispatcher.Endpoints)
                {
                    if ( endpoint.ContractName == MexContractName && endpoint.ContractNamespace == MexContractNamespace ) {
                        DispatchRuntime dispatchRuntime = endpoint.DispatchRuntime;
                        dispatchRuntime.InstanceContextProvider = Utility.CreateInstance<IInstanceContextProvider>(
                            SingletonInstanceContextProviderType,
                            new Type[] { typeof(DispatchRuntime) }, new object[] { dispatchRuntime });
                        MetadataProvisionService serviceInstance = new MetadataProvisionService(metadata);
                        dispatchRuntime.SingletonInstanceContext = new InstanceContext(host,serviceInstance);
                    }
                }
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
           
        }

        
    }
}
