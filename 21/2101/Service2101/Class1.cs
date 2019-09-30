using Service.Interface2101;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Routing;
using System.Text;
using System.Threading.Tasks;

namespace Service2101
{
    public class HelloService : IHello
    {
        public string SayHello(string userName)
        {
            return $"{userName } say Hello";
            
        }
    }

    public class GoodByeService : IGoodBye
    {
        public string SayGoodBye(string userName)
        {
            return $"{userName } say GoodBye";
        }
    }
}
