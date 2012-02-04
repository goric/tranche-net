using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractSyntaxTree
{
    public class BinaryExpression : Expression
    {
        public Expression Left { get; set; }
        public Expression Right { get; set; }

        public BinaryExpression(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override void Visit (Visitor v)
        {
            v.VisitBinaryExpression(this);
        }
    }
}
