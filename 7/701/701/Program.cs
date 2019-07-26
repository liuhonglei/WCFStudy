using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.Text;

namespace _701
{
    class Program
    {
        static void Main(string[] args)
        {
           var description =  CreateDescription(typeof(CalculatorService));

        }

        private static ServiceDescription   CreateDescription(Type serviceType)
        {
            ServiceDescription description = new ServiceDescription();
            description.ServiceType = serviceType;
            var behaivors = (from attribute in serviceType.GetCustomAttributes(false)
                             where attribute is IServiceBehavior
                             select (IServiceBehavior)attribute).ToArray();

            Array.ForEach<IServiceBehavior>(behaivors, behaivor => description.Behaviors.Add(behaivor));

            ServiceBehaviorAttribute serviceBehaviorAttribute =
                description.Behaviors.Find<ServiceBehaviorAttribute>();
            if (null == serviceBehaviorAttribute)
            {
                serviceBehaviorAttribute = new ServiceBehaviorAttribute();
                description.Behaviors.Add(serviceBehaviorAttribute);
            }
            description.Name = serviceBehaviorAttribute.Name ?? serviceType.Name;
            description.Namespace = serviceBehaviorAttribute.Namespace ?? "http://www.tempri.org";
            description.ConfigurationName = serviceBehaviorAttribute.ConfigurationName ?? description.Namespace + "." + description.Name;

            //添加服务行为
            ServiceElement serviceElement =  ConfigLoader.GetServiceElement(description.ConfigurationName);
            if ( !string.IsNullOrEmpty(serviceElement.BehaviorConfiguration) ) {
                ServiceBehaviorElement behaviorElement =
                    ConfigLoader.GetServiceBehaviorElement(serviceElement.BehaviorConfiguration);
                foreach (BehaviorExtensionElement  item in behaviorElement)
                {
                    IServiceBehavior serviceBehavior = item.CreateBehavior() as IServiceBehavior;
                    description.Behaviors.Add(serviceBehavior);
                }
            }

            foreach (ServiceEndpointElement item in serviceElement.Endpoints)
            {
                description.Endpoints.Add(CreateServiceEndpoint(serviceType, item));
            }

            return description;
        }

        private static ServiceEndpoint CreateServiceEndpoint(Type serviceType, ServiceEndpointElement item)
        {
            throw new NotImplementedException();
        }
    }

    public static class ConfigLoader
    {

        public static ServiceElement GetServiceElement(string serviceName)
        {
            //ServiceDescription.GetService
            return new ServiceElement();
        }

        public static ServiceBehaviorElement GetServiceBehaviorElement(string mame)
        {
            throw new NotImplementedException();
        }

        public static EndpointBehaviorElement GetEndpointBehaviorElement(string mame)
        {
            throw new NotImplementedException();
        }

        public static Binding CreateBinding(string bindingName)
        {
            throw new NotImplementedException();
        }

        public static object CreateBehavior(this BehaviorExtensionElement behaviorExtensionElement)
        {
            throw new NotImplementedException();
        }
}
}
