using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Service.Inteface
{
    [ServiceContract(Namespace = "http://www.lhl.com", CallbackContract=typeof(ICalculatorCallBack))]
    public interface ICalculator
    {
        [OperationContract(IsOneWay = true)]
        void Add(double a, double b);
    }
}
