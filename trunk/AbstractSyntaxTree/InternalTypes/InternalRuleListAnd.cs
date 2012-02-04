using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using QUT.Gppg;

namespace AbstractSyntaxTree.InternalTypes
{
    public class InternalRuleListAnd : ExpressionList
    {
        public InternalRuleListAnd () { IsEmpty = true; }
        public InternalRuleListAnd (Expression head, ExpressionList tail)
        {
            Head = head;
            Tail = tail;
            IsEmpty = false;
        }

        public override void Visit (Visitor v)
        {
            v.VisitInternalRuleListAnd(this);
        }

        public override string Print (int depth)
        {
            if (IsEmpty)
                return string.Empty;
            return " and " + Head.Print(depth) + NewLine(depth) + Tail.Print(depth);
        }
    }
}
