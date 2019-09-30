using Service.Interface2103;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service2103
{
    public class CalculatorService : ICalculator
    {
        public int Add(int x, int y)
        {
            return x + y;
        }
    }
}
