using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractSyntaxTree.InternalTypes
{
    public class Rules : StatementList
    {
        public InternalRuleList Statements { get; set; }

        public Rules (InternalRuleList items)
        {
            Statements = items;
        }
        
        public override void Visit (Visitor v)
        {
            v.VisitRules(this);
        }

        public override string Print (int depth)
        {
            if (IsEmpty)
                return string.Empty;

            return "Rules {" + NewLine(depth + 1) + Statements.Print(depth + 1) + NewLine(depth) + "}";
        }
    }
}
