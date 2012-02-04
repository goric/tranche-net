using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractSyntaxTree.InternalTypes
{
    public class PrincipalRules : Statement
    {
        public Expression Statements { get; set; }

        public PrincipalRules (Expression stmt)
        {
            Statements = stmt;
        }
        
        public override void Visit (Visitor v)
        {
            v.VisitPrincipalRules(this);
        }

        public override string Print (int depth)
        {
            return "Principal {" + NewLine(depth + 1) + Statements.Print(depth + 1) + NewLine(depth) + "}" + NewLine(depth);
        }
    }
}
