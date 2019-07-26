using Interface901;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace Service901
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class CalculatorService : ICalculator,IDisposable
    {
        public CalculatorService() {

            Console.WriteLine("构造 方法被调用 {0}", Thread.CurrentThread.ManagedThreadId);
        }
        ~CalculatorService()
        {
            Console.WriteLine("终结器 方法被调用 {0}", Thread.CurrentThread.ManagedThreadId);
        }
        public double Add(double x, double y)
        {
            Console.WriteLine("add 方法被调用 {0}", Thread.CurrentThread.ManagedThreadId);
            return x + y;
        }
        public void Dispose()
        {
            Console.WriteLine("dispose 方法被调用 {0}", Thread.CurrentThread.ManagedThreadId);
        }
    }
}
