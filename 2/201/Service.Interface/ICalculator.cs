using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Service.Interface
{
    [ServiceContract(Name ="CalculatorService" ,Namespace="http://www.lhl.com")]
    public interface ICalculator
    {

        [OperationContract]
        double Add(double x,double y);
    }
}
