using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MessageDescriptionDemo
{
    [ServiceContract(Namespace = "http://www.lhl.com")]
    interface ICalculator3
    {
        [OperationContract( IsOneWay = true)]
        void Add(double a, double b);
    }
}
