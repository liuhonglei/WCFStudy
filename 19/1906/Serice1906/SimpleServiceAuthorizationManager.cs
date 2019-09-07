using System.IdentityModel.Claims;
using System.Security.Principal;
using System.ServiceModel;

namespace Serice1906
{
    public class SimpleServiceAuthorizationManager : ServiceAuthorizationManager
    {
        public SimpleServiceAuthorizationManager()
        {
        }

        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            string action = operationContext.RequestContext.RequestMessage.Headers.Action;
            foreach (ClaimSet claimSet in operationContext.ServiceSecurityContext.AuthorizationContext.ClaimSets)
            {
                if (claimSet.Issuer == ClaimSet.System)
                {
                    foreach (Claim c in claimSet.FindClaims(SimpleAuthorizationPolicy.ClaimType4AllowedOperation,Rights.PossessProperty))
                    {
                        if (action == c.Resource.ToString())
                        {
                            GenericIdentity identity = new GenericIdentity("");

                            operationContext.ServiceSecurityContext.AuthorizationContext.Properties["Principal"]
                                = new GenericPrincipal(identity, null);
                            return true;
                        }
                    }
                }
            }
            return false;
            //return base.CheckAccessCore(operationContext);
        }
    }
}
