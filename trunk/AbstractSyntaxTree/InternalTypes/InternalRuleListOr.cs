using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using QUT.Gppg;

namespace AbstractSyntaxTree.InternalTypes
{
    public class InternalRuleListOr : ExpressionList
    {
        public InternalRuleListOr () { IsEmpty = true; }
        public InternalRuleListOr (Expression head, ExpressionList tail)
        {
            Head = head;
            Tail = tail;
            IsEmpty = false;
        }

        public override void Visit (Visitor v)
        {
            v.VisitInternalRuleListOr(this);
        }

        public override string Print (int depth)
        {
            if (IsEmpty)
                return string.Empty;
            return " or " + Head.Print(depth) + NewLine(depth) + Tail.Print(depth);
        }
    }
}
