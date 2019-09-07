using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Interface1907
{
    [ServiceContract]
    public interface ICalculator
    {
        [OperationContract]
        void Add(double x, double y);

    }
}
