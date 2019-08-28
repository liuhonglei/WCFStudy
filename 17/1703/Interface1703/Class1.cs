using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Interface1703
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IGreeting
    {
        [OperationContract( IsOneWay = true )]
        void SayHello(string name);
        [OperationContract(IsOneWay = true)]
        void SayGoodBye(string name);
    }
}
