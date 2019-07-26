using Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace Client1302
{
    



    [ServiceMetadataBehavior]
    public class CalculatorService : ICalculator1,IMetadataProvisionService
    {
        public double Add(double x, double y)
        {
            throw new NotImplementedException();
        }

        public Message Get(Message request)
        {
            throw new NotImplementedException();
        }

        public double Substract(double x, double y)
        {
            throw new NotImplementedException();
        }
    }
}
