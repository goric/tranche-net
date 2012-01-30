using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractSyntaxTree.InternalTypes
{
    public class CollateralItem : StatementList
    {
        public StatementList Statements { get; set; }

        public CollateralItem () { IsEmpty = true; }
        public CollateralItem (StatementList head, StatementList tail)
        {
            IsEmpty = false;
            Statements = head;
            Tail = tail;
        }
        
        public override void Visit (Visitor v)
        {
            v.VisitCollateralItem(this);
        }

        public override string Print (int depth)
        {
            if (IsEmpty)
                return string.Empty;

            return "CollateralItem {" + NewLine(depth + 1) + (IsEmpty ? string.Empty : Statements.Print(depth + 1)) + NewLine(depth) + "}"
                + NewLine(depth) + (Tail == null ? string.Empty : Tail.Print(depth));
        }
    }
}
