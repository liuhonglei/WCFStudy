using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MessageDescriptionDemo
{
    [ServiceContract(Namespace = "http://www.lhl.com")]
    interface ICalculator2
    {
        [OperationContract]
        void Add(ref double a,ref double b, out double c);
    }
}
