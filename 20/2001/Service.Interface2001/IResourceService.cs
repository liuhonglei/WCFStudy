using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface2001
{
    [ServiceContract(Namespace = "http://www.lhl.com")]
    //[CulturePropagationBehavior]
    public interface IResourceService
    {
        [OperationContract]
        string GetString(string key);
    }
}
