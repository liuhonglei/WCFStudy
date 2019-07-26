using Interface1303;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service1303
{
    
    public class CalculatorService : ICalculator1
    {
        public double Add(double x, double y)
        {
            return x + y;
        }

        public double Substract(double x, double y)
        {
            throw new NotImplementedException();
        }
    }
}
