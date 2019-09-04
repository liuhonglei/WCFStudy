using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

            

            //IPrincipal principal = Thread.CurrentPrincipal;
            //if (principal.IsInRole("Adminstrator"))
            //{
            //    //执行授权操作
            //}
            //else {
            //    //异常
            //}




            Console.ReadKey();
        }
    }

}
