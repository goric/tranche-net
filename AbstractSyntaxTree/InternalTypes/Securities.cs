using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractSyntaxTree.InternalTypes
{
    public class Securities : Statement
    {
        public StatementList Statements { get; set; }

        public Securities (StatementList stmt)
        {
            Statements = stmt;
        }
        
        public override void Visit (Visitor v)
        {
            v.VisitSecurities(this);
        }

        public override string Print (int depth)
        {
            return "Securities {" + NewLine(depth + 1) + Statements.Print(depth + 1) + NewLine(depth) + "}";
        }
    }
}
