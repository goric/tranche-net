using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using QUT.Gppg;

namespace AbstractSyntaxTree.InternalTypes
{
    public class InternalRuleList : ExpressionList
    {
        public string Joiner { get; set; }

        public InternalRuleList () { IsEmpty = true; }
        public InternalRuleList (Expression head, ExpressionList tail)
        {
            Head = head;
            Tail = tail;
            IsEmpty = false;
        }

        public override void Visit (Visitor v)
        {
            v.VisitInternalRuleList(this);
        }

        public override string Print (int depth)
        {
            if (IsEmpty)
                return string.Empty;
            return "->" + Head.Print(depth)
                + (Tail.IsEmpty || Tail is InternalRuleListAnd || Tail is InternalRuleListOr ? string.Empty : NewLine(depth))
                + Tail.Print(depth);
        }
    }
}
