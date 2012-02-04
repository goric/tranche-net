using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;

namespace AbstractSyntaxTree
{
    public class GreaterEqual : BinaryExpression
    {
        public GreaterEqual (Expression l, Expression r) : base(l, r) {}

        public override string Print (int depth)
        {
            return Left.Print(depth) + " >= " + Right.Print(depth);
        }

        public override void Visit (Visitor v)
        {
            v.VisitGreaterEqual(this);
        }
    }
}
