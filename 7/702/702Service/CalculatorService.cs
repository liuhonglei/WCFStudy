using _702Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.ServiceModel.Description;
using System.Web;

namespace _702Service
{
    [AspNetCompatibilityRequirementsAttribute(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed) ]
    public class CalculatorService : ICalculator
    {
        
        public double Add(double a, double b)
        {
            return a + b;
        }
    }
}