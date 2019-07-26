using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;

namespace Service1001
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebServiceHost webServiceHost = new WebServiceHost(typeof(EmployeesService))) {
                foreach (ServiceEndpoint endpoint in webServiceHost.Description.Endpoints)
                {
                    WebHttpBehavior webHttpBehavior = endpoint.Behaviors.Find<WebHttpBehavior>();
                    if (null != webHttpBehavior)
                        endpoint.Behaviors.Remove(webHttpBehavior);
                }

                webServiceHost.Open();

                foreach (ServiceEndpoint endpoint in webServiceHost.Description.Endpoints)
                {
                    Debug.Assert(null != endpoint.Behaviors.Find<WebHttpBehavior>());
                }
                 Console.Read();
            }
        }
    }
}
