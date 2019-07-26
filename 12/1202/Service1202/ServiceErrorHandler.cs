using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace Service1202
{
         public class ServiceErrorHandler : IErrorHandler
         {
            public string ExceptionPolicyName
            { get; private set; }
            public ServiceErrorHandler(string exceptionPolicyName)
            {
                if (string.IsNullOrEmpty(exceptionPolicyName))
                {
                    throw new ArgumentNullException("exceptionPolicyName");
                }
                this.ExceptionPolicyName = exceptionPolicyName;
            }
     
            #region IErrorHandler Members
            public bool HandleError(Exception error)
            {
                return false;
            }
            public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
            {
                if(typeof(FaultException).IsInstanceOfType(error))
                {
                    return;
                }
     
                try
                {
                    if (ExceptionPolicy.HandleException(error, this.ExceptionPolicyName))
                    {
                         fault = Message.CreateMessage(version, BuildFault(error), ServiceExceptionDetail.FaultAction);
                     }
                }
                catch (Exception ex)
                {
                    fault = Message.CreateMessage(version, BuildFault(ex), ServiceExceptionDetail.FaultAction);
                }
            }
     
            private MessageFault BuildFault(Exception error)
            {           
                ServiceExceptionDetail exceptionDetail = new ServiceExceptionDetail(error);
                return MessageFault.CreateFault(FaultCode.CreateReceiverFaultCode(ServiceExceptionDetail.FaultSubCodeName, ServiceExceptionDetail.FaultSubCodeNamespace),
                    new FaultReason(error.Message), exceptionDetail);
            }
     
            #endregion
        }

}
