using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractSyntaxTree
{
    public class Equal : BinaryExpression
    {
        public Equal (Expression l, Expression r) : base(l, r) {}

        public override string Print (int depth)
        {
            return Left.Print(depth) + " == " + Right.Print(depth);
        }

        public override void Visit (Visitor v)
        {
            v.VisitEqual(this);
        }
    }
}
