using Interface1906;
using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serice1906
{
    public class CalculatorService : ICalculator
    {
        public double Add(double x, double y)
        {
            return x + y;
        }

        public double Divide(double x, double y)
        {
            return x - y;
        }

        public double Multiply(double x, double y)
        {
            return x * y;
        }

        public double Subtract(double x, double y)
        {
            return x / y;
        }
    }

    public class SimpleAuthorizationPolicy : IAuthorizationPolicy
    {
        const string ActionOfAdd = "http://www.lhl.com/calculator/add";
        const string ActionOfSubtract = "http://www.lhl.com/calculator/subtract";
        const string ActionOfMultiply = "http://www.lhl.com/calculator/multiply";
        const string ActionOfDivide = "http://www.lhl.com/calculator/divide";

        internal const string ClaimType4AllowedOperation = "http://www.lhl.com/allowed";

        public SimpleAuthorizationPolicy() {
            this.Id = Guid.NewGuid().ToString();

        }

        public ClaimSet Issuer => ClaimSet.System;

        public string Id { get; set; }

        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            if (null == state)
            {
                state = false;
            }
            bool hasAddedClaims = (bool)state;
            if (hasAddedClaims)
                return true;
            IList<Claim> claims = new List<Claim>();

            foreach (ClaimSet claimSet in evaluationContext.ClaimSets)
            {
                foreach (Claim claim in claimSet.FindClaims(ClaimTypes.Name,Rights.PossessProperty))
                {
                    string userName = (string)claim.Resource;

                    if (userName.Contains('\\'))
                    {
                        userName = userName.Split('\\')[1];
                        if (string.Compare("Foo",userName,true) == 0)
                        {
                            claims.Add(new Claim(ClaimType4AllowedOperation,ActionOfAdd,Rights.PossessProperty));
                            claims.Add(new Claim(ClaimType4AllowedOperation, ActionOfSubtract, Rights.PossessProperty));
                        }
                        if (string.Compare("Bar", userName, true) == 0)
                        {
                            claims.Add(new Claim(ClaimType4AllowedOperation, ActionOfMultiply, Rights.PossessProperty));
                            claims.Add(new Claim(ClaimType4AllowedOperation, ActionOfDivide, Rights.PossessProperty));
                        }
                    }
                }
            }
            evaluationContext.AddClaimSet(this, new DefaultClaimSet(this.Issuer, claims));
            state = true;
            return true;
        }
    }
}
