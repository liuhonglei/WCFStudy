using Interface1501;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace Service1501
{
    [ServiceBehavior(UseSynchronizationContext = false, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CalculatorService : ICalculator
    {
        public double Add(double a, double b)
        {
            EventMonitor.Send(EventType.StartExecute);
            Thread.Sleep(5000);
            double result = a + b;
            EventMonitor.Send( EventType.EndExecute);
            return result;
        }
    }
}
