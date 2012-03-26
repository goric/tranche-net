using System;

namespace AbstractSyntaxTree
{
    public class Increment : Expression
    {
        public Expression Expression { get; set; }

        public Increment (Expression inc)
        {
            Expression = inc;
        }

        public override String Print (int depth)
        {
            return Expression.Print(depth) + "++";
        }

        public override void Visit (Visitor v)
        {
            v.VisitIncrement(this);
        }
    }
}
