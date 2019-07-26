using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace _702Service.Interface
{
    [ServiceContract(Namespace = "http://www.lhl.com")]
    public interface ICalculator
    {
        //[OperationContract(IsOneWay = true)]
        [OperationContract]
        double Add(double a, double b);
    }
}
