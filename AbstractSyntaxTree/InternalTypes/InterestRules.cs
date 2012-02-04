using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractSyntaxTree.InternalTypes
{
    public class InterestRules : Statement
    {
        public Expression Statements { get; set; }

        public InterestRules () { }
        public InterestRules (Expression stmt)
        {
            Statements = stmt;
        }
        
        public override void Visit (Visitor v)
        {
            v.VisitInterestRules(this);
        }

        public override string Print (int depth)
        {
            if (Statements == null)
                return string.Empty;
            return "Interest {" + NewLine(depth + 1) + Statements.Print(depth + 1) + NewLine(depth) + "}" + NewLine(depth);
        }
    }
}
