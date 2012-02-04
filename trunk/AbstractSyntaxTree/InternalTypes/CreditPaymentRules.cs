using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractSyntaxTree.InternalTypes
{
    public class CreditPaymentRules : Statement
    {
        public InterestRules Interest { get; set; }
        public PrincipalRules Principal { get; set; }

        public CreditPaymentRules () { }
        public CreditPaymentRules (InterestRules interest, PrincipalRules prin)
        {
            Interest = interest;
            Principal = prin;
        }
        
        public override void Visit (Visitor v)
        {
            v.VisitCreditPaymentRules(this);
        }

        public override string Print (int depth)
        {
            return "CreditPaymentRules {" + NewLine(depth + 1) + Interest.Print(depth + 1) + Principal.Print(depth+1) + NewLine(depth) + "}";
        }
    }
}
