using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace _701
{
    [ServiceContract(Namespace = "http://www.lhl.com" ,ConfigurationName = "ICalculator1")]
    [SimpleContractBehavior]
    interface ICalculator1
    {
        [OperationContract]
        double Add(double x,double y);
        [OperationContract]
        double Substract(double x, double y);
    }

    [ServiceContract(Namespace = "http://www.lhl.com", ConfigurationName = "ICalculator2")]
    [SimpleContractBehavior]
    interface ICalculator2
    {
        [OperationContract]
        [SimpleOperationBehavior]
        double Multiply(double x, double y);
        [OperationContract]
        double Divide(double x, double y);
    }
}
