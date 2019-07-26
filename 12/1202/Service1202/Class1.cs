using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service1202
{
    [DataContract(Namespace = "http://www.lhl.com")]
    public class ServiceExceptionDetail : ExceptionDetail
    {
        public const string FaultSubcodeNamespace = "http://www.lhl.com/exceptionhandling";
        public const string FaultSubcodeName = "servererror";
        public const string FaultAction = "http://www.lhl.com/fault";

        [DataMember]
        public string AssemblyQualifiedName { get; private set; }

        [DataMember]
        public new ServiceExceptionDetail InnerException { get; private set; }

        public ServiceExceptionDetail(Exception exception) : base(exception)
        {
            this.AssemblyQualifiedName = exception.GetType().AssemblyQualifiedName;
            if (null != exception.InnerException)
                InnerException = new ServiceExceptionDetail(exception.InnerException);
        }

        public override string ToString()
        {
            return this.Message;
        }
    }
}
