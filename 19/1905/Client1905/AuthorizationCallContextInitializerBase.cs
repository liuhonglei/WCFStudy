using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Security;

namespace Client1905
{
    public abstract class AuthorizationCallContextInitializerBase : ICallContextInitializer
    {
        public void AfterInvoke(object correlationState)
        {
            IPrincipal principal = correlationState as IPrincipal;
            if (null != principal)
            {
                Thread.CurrentPrincipal = principal;
            }
        }

        public object BeforeInvoke(InstanceContext instanceContext, IClientChannel channel, Message message)
        {
            var originalPrincipal = Thread.CurrentPrincipal;
            Thread.CurrentPrincipal = this.GetPrincipal(ServiceSecurityContext.Current);
            return originalPrincipal;
        }

        protected abstract IPrincipal GetPrincipal(ServiceSecurityContext context);
    }

    public class WindowsAuthorizationCallContextInitializer : AuthorizationCallContextInitializerBase
    {
        protected override IPrincipal GetPrincipal(ServiceSecurityContext context)
        {
            WindowsIdentity windowsIdentity = context.WindowsIdentity;
            if (null == windowsIdentity)
                windowsIdentity = WindowsIdentity.GetAnonymous();
            return new WindowsPrincipal(windowsIdentity);
        }
    }

    public class AspNetRolesAuthorizationCallContextInitializer : AuthorizationCallContextInitializerBase
    {
        public RoleProvider RolePrivider { get; private set; }

        public AspNetRolesAuthorizationCallContextInitializer(RoleProvider rolePrivider) {
            RolePrivider = rolePrivider;
        }
        protected override IPrincipal GetPrincipal(ServiceSecurityContext context)
        {
            var userName = context.PrimaryIdentity.Name;
            var identity = new GenericIdentity(userName);
            var roles = RolePrivider.GetRolesForUser(userName);
            return new GenericPrincipal(identity,roles);

        }
    }
}
