using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface2102
{
    [ServiceContract(Namespace = "http://www.lhl.com")]
    public interface ICalculator
    {
        [OperationContract]
        int Add(int x, int y);
    }
}
