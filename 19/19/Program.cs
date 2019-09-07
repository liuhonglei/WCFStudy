using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Security;

namespace _19
{
    class Program
    {
        static void Main(string[] args)
        {
            WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent();

            GenericIdentity anoymousIdentity = new GenericIdentity("");
            GenericIdentity authenticIdentity = new GenericIdentity("Foo");
            Console.WriteLine(anoymousIdentity.IsAuthenticated); //false
            Console.WriteLine(authenticIdentity.IsAuthenticated); //true

            var identity1 = Thread.CurrentPrincipal.Identity;
            var identity2 = ServiceSecurityContext.Current.PrimaryIdentity;

            Debug.Assert(object.ReferenceEquals(identity1, identity2));
            //IPrincipal principal = Thread.CurrentPrincipal;
            //if (principal.IsInRole("Adminstrator"))
            //{
            //    //执行授权操作
            //}
            //else {
            //    //异常
            //}

            //wcf  Asp.net Roles 授权模式
            using (ServiceHost host = new ServiceHost(typeof(CalculatorService)))
            {
                ServiceAuthorizationBehavior serviceAuthorizationBehavior = host.Description.Behaviors.Find<ServiceAuthorizationBehavior>();
                if (null == serviceAuthorizationBehavior) {
                    serviceAuthorizationBehavior = new ServiceAuthorizationBehavior();
                    host.Description.Behaviors.Add(serviceAuthorizationBehavior);
                }
                serviceAuthorizationBehavior.PrincipalPermissionMode = PrincipalPermissionMode.UseAspNetRoles;
                serviceAuthorizationBehavior.RoleProvider = Roles.Provider;
                host.Open();
            }

            DispatchRuntime

                Console.ReadKey();
        }
    }

    internal class CalculatorService
    {
    }
}
