using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace Service903
{
    class SingletonInstanceContextProvider : IInstanceContextProvider
    {
        private DispatchRuntime runtime;
        public SingletonInstanceContextProvider(DispatchRuntime dispatchRuntime) {
            runtime = dispatchRuntime;
        }
        public InstanceContext GetExistingInstanceContext(Message message, IContextChannel channel)
        {
            return runtime.SingletonInstanceContext;
        }

        public void InitializeInstanceContext(InstanceContext instanceContext, Message message, IContextChannel channel)
        {

        }

        public bool IsIdle(InstanceContext instanceContext)
        {
            return false;
        }

        public void NotifyIdle(InstanceContextIdleCallback callback, InstanceContext instanceContext)
        {
            throw new NotImplementedException();
        }
    }

    public class SingletonAttribute : Attribute, IServiceBehavior
    {
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
           
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher channelDispatcher in serviceHostBase.ChannelDispatchers)
            {
                foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
                {
                   DispatchRuntime runtime = endpointDispatcher.DispatchRuntime;
                    ServiceHost serviceHost = serviceHostBase as ServiceHost;
                    if (null != serviceHost.SingletonInstance)
                    {
                        runtime.SingletonInstanceContext = new InstanceContext(serviceHostBase, serviceHost.SingletonInstance);
                        SetDisposeInstance(serviceHost, serviceHost.SingletonInstance);

                    }
                    else {
                        object serviceInstance = Activator.CreateInstance(serviceDescription.ServiceType);
                        runtime.SingletonInstanceContext = new InstanceContext(serviceInstance);
                        SetDisposeInstance(serviceHost, serviceInstance);
                    }
                }

            }

        }

        private void SetDisposeInstance(ServiceHost serviceHost, object singletonInstance)
        {
            if (!singletonInstance.GetType().GetInterfaces().Contains(typeof(IDisposable)))
                return;
            FieldInfo field = typeof(ServiceHost).GetField("disposableInstance", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            field.SetValue(serviceHost, singletonInstance);
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            
        }
    }
}
