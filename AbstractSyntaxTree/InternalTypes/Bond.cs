using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractSyntaxTree.InternalTypes
{
    public class Bond : StatementList
    {
        public StatementList Statements { get; set; }

        public Bond () { IsEmpty = true; }
        public Bond (StatementList items, StatementList tail)
        {
            IsEmpty = false;
            Statements = items;
            Tail = tail;
        }
        
        public override void Visit (Visitor v)
        {
            v.VisitBond(this);
        }

        public override string Print (int depth)
        {
            if (IsEmpty)
                return string.Empty;

            return "Bond {" + NewLine(depth + 1) + (IsEmpty ? string.Empty : Statements.Print(depth + 1)) + NewLine(depth) + "}"
                + NewLine(depth) + (Tail == null ? string.Empty : Tail.Print(depth));
        }
    }
}
