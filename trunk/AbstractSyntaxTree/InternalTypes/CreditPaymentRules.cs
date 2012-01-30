using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractSyntaxTree.InternalTypes
{
    public class CreditPaymentRules : Statement
    {
        public StatementList Statements { get; set; }

        public CreditPaymentRules () { }
        public CreditPaymentRules (StatementList stmt)
        {
            Statements = stmt;
        }
        
        public override void Visit (Visitor v)
        {
            v.VisitCreditPaymentRules(this);
        }

        public override string Print (int depth)
        {
            return "CreditPaymentRules {" + NewLine(depth + 1) + Statements.Print(depth + 1) + NewLine(depth + 1) + "}";
        }
    }
}
