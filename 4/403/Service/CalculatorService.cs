using Service.Inteface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    //[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class CalculatorService : ICalculator
    {

        public void Add(double x, double y)
        {
            double result = x + y;
            ICalculatorCallBack callback = OperationContext.Current.GetCallbackChannel<ICalculatorCallBack>();
            callback.DisplayResult(result, x,y);
        }
    }
}
