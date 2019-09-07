using Interface1907;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service1907
{
    public class CalculatorService : ICalculator
    {
        public void Add(double x, double y)
        {
            Console.WriteLine(x+y);  
        }
    }
}
