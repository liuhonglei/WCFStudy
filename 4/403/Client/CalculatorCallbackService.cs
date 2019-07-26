using Service.Inteface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class CalculatorCallbackService : ICalculatorCallBack
    {
        
        public void DisplayResult(double result, double x, double y)
        {
            Console.WriteLine("x +  y = {2} ,x = {0} ,y = {1} ",x,y,result);
        }
    }
}
