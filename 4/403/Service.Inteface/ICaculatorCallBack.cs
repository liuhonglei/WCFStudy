using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Service.Inteface
{
    public interface ICalculatorCallBack
    {
        [OperationContract(IsOneWay = true)]
        void DisplayResult(double result,double x,double y);

    }
}
