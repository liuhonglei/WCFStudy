using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Interface
{
    public  class CalculatorService : ICalculator
    {
        public double Add(double x, double y)
        {
            return x + y;
        }
    }
}
