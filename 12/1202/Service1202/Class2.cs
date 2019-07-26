using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace Service1202
{
    //public class ExceptionHandlingMessageInspector : IClientMessageInspector
    //     {
    //         public void AfterReceiveReply(ref Message reply, object correlationState)
    //        {
    //            if (!reply.IsFault)
    //            {
    //                return;
    //            }

    //            if (reply.Headers.Action == ServiceExceptionDetail.FaultAction)
    //            {
    //                MessageFault fault = MessageFault.CreateFault(reply, int.MaxValue);
    //                if(fault.Code.SubCode.Name == ServiceExceptionDetail.FaultSubCodeName &&
    //                    fault.Code.SubCode.Namespace == ServiceExceptionDetail.FaultSubCodeNamespace)
    //                {
    //                    FaultException<ServiceExceptionDetail> exception = (FaultException<ServiceExceptionDetail>)FaultException.CreateFault(fault, typeof(ServiceExceptionDetail));
    //                    throw GetException(exception.Detail);
    //                }
    //            }
    //        }

    //        private Exception GetException(ServiceExceptionDetail exceptionDetail)
    //        {
    //            if (null == exceptionDetail.InnerException)
    //            {
    //                return (Exception) Activator.CreateInstance(Type.GetType(exceptionDetail.AssemblyQualifiedName), exceptionDetail.Message);
    //            }

    //            Exception innerException = GetException(exceptionDetail.InnerException);
    //            return (Exception) Activator.CreateInstance(Type.GetType(exceptionDetail.AssemblyQualifiedName), exceptionDetail.Message, innerException);
    //        }

    //        public object BeforeSendRequest(ref Message request, IClientChannel channel)
    //        {
    //            return null;
    //        }
    //    }


    public class ExceptionHandlingBehaviorAttribute : Attribute, IOperationBehavior, IContractBehavior, IEndpointBehavior, IServiceBehavior
        {
            public string ExceptionPolicyName
            { get; private set; }
     
            public ExceptionHandlingBehaviorAttribute(string exceptionPolicyName)
            {
                if (string.IsNullOrEmpty(exceptionPolicyName))
                {
                    throw new ArgumentNullException("exceptionPolicyName");
                }
                this.ExceptionPolicyName = exceptionPolicyName;
            }
     
            #region IOperationBehavior Members
            public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters) { }
            public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
            {
                clientOperation.Parent.MessageInspectors.Add(new ExceptionHandlingMessageInspector());
            }
            public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
            {
                dispatchOperation.Parent.ChannelDispatcher.ErrorHandlers.Add(new ServiceErrorHandler(this.ExceptionPolicyName));
                dispatchOperation.Parent.ChannelDispatcher.ErrorHandlers.Add(new ServiceErrorHandler(this.ExceptionPolicyName));
            }
            public void Validate(OperationDescription operationDescription) { }
            #endregion
     
            #region IEndpointBehavior Members
            public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
            {}
            public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
            {
                clientRuntime.MessageInspectors.Add(new ExceptionHandlingMessageInspector());
            }
            public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher) { }
            public void Validate(ServiceEndpoint endpoint) { }
            #endregion
     
            #region IServiceBehavior Members
            public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
            {}
            public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
            {
                foreach (ChannelDispatcher channelDispatcher in serviceHostBase.ChannelDispatchers)
                {              
                    channelDispatcher.ErrorHandlers.Add(new ServiceErrorHandler(this.ExceptionPolicyName));
                }
            }
            public void Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase) { }
            #endregion
     
            #region IContractBehavior Members
            public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters) { }
            public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
            {
                clientRuntime.MessageInspectors.Add(new ExceptionHandlingMessageInspector());
            }
     
            public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
            {
                dispatchRuntime.ChannelDispatcher.ErrorHandlers.Add(new ServiceErrorHandler(this.ExceptionPolicyName));
            }
            public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
            {}
            #endregion
        }

}
