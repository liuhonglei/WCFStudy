using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface2101
{
    [ServiceContract(Namespace = "http://www.lhl.com/")]
    public interface IHello
    {
        [OperationContract]
        string SayHello(string userName);
    }
}
