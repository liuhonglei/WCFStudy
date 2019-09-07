using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Interface1906
{
    [ServiceContract(Namespace = "http://www.lhl.com")]
    public interface ICalculator
    {
        [OperationContract(Action = "http://www.lhl.com/calculator/add")]
        double Add(double x, double y);
        [OperationContract(Action = "http://www.lhl.com/calculator/subtract")]
        double Subtract(double x, double y);
        [OperationContract(Action = "http://www.lhl.com/calculator/multiply")]
        double Multiply(double x, double y);
        [OperationContract(Action = "http://www.lhl.com/calculator/divide")]
        double Divide(double x, double y);
    }
}
