using Interface1802;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service1802
{
    public class CalculatorService : ICalculator
    {
        public double Add(double a, double b)
        {
            Console.WriteLine("add 操作开始执行！");
            return a + b;
        }
    }
}
