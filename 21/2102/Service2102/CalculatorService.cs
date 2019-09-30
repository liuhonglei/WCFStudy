using Service.Interface2102;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service2102
{
   public  class CalculatorService : ICalculator
    {
        public int Add(int x, int y)
        {
            return x + y;
        }
    }
}
