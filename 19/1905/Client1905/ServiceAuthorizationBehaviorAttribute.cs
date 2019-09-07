using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Client1905
{

    [System.AttributeUsage(AttributeTargets.Class)]
    sealed class ServiceAuthorizationBehaviorAttribute : Attribute,IServiceBehavior
    {
        public PrincipalPermissionMode PrincipalPermissionMode { get; private set; }
        public ICallContextInitializer CallContextInitializer { get; private set; }

        public ServiceAuthorizationBehaviorAttribute(PrincipalPermissionMode principalPermissionMode , string rolePrividername = "")
        {
            switch (principalPermissionMode) {
                case PrincipalPermissionMode.UseWindowsGroups:
                {
                        this.CallContextInitializer = new WindowsAuthorizationCallContextInitializer();
                        break;
                }
                case PrincipalPermissionMode.UseAspNetRoles:
                {
                        if (string.IsNullOrEmpty(rolePrividername))
                            this.CallContextInitializer = new AspNetRolesAuthorizationCallContextInitializer(Roles.Provider);
                        else
                            this.CallContextInitializer = new AspNetRolesAuthorizationCallContextInitializer(Roles.Providers[rolePrividername]);
                        break;
                }
                case PrincipalPermissionMode.Custom:
                    {

                        throw new ArgumentException("只支持UseWindowsGroup和AspNetRoles模式");
                    }

            }

        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
            
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            if (null == CallContextInitializer)
                return;
            foreach (ChannelDispatcher channelDispatcher in serviceHostBase.ChannelDispatchers)
            {
                foreach (EndpointDispatcher endPoint in channelDispatcher.Endpoints)
                {
                    
                    foreach (DispatchOperation operation in endPoint.DispatchRuntime.Operations)
                    {
                        operation.CallContextInitializers.Add(this.CallContextInitializer);
                       
                    }
                }
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
           
        }
    }

    [ServiceContract]
    public interface ICalculator {

        [OperationContract]
        double Add(double x, double y);
    }

    //[ServiceBehavior( ConcurrencyMode = ConcurrencyMode.Reentrant )]
    [ServiceAuthorizationBehavior( PrincipalPermissionMode.UseAspNetRoles)]
    public class CalculatorService : ICalculator
    {
        public double Add(double x, double y)
        {
            return x +y;
        }
    }
}
