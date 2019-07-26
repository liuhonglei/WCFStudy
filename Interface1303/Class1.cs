using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Interface1303
{
    [ServiceContract(Namespace = "http://www.lhl.com")]
    public interface ICalculator1
    {
        [OperationContract]
        double Add(double x, double y);
        [OperationContract]
        double Substract(double x, double y);
    }
}
