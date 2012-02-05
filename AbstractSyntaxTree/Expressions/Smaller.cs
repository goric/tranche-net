﻿
namespace AbstractSyntaxTree
{
    public class Smaller : BinaryExpression
    {
        public Smaller (Expression l, Expression r) : base(l, r) {}

        public override string Print (int depth)
        {
            return Left.Print(depth) + " < " + Right.Print(depth);
        }

        public override void Visit (Visitor v)
        {
            v.VisitSmaller(this);
        }
    }
}
