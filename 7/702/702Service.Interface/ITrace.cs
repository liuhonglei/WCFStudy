using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace _702Service.Interface
{
    [ServiceContract]
    public  interface ITrace
    {
        [OperationContract(IsOneWay = true)]
        void Write(string message);
        [OperationContract(IsOneWay = true)]
        void WriteLine(string message);
    }
}
