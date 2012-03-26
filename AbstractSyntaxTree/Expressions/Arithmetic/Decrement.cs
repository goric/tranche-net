using System;

namespace AbstractSyntaxTree
{
    public class Decrement : Expression
    {
        public Expression Expression { get; set; }

        public Decrement(Expression inc)
        {
            Expression = inc;
        }

        public override String Print(int depth)
        {
            return Expression.Print(depth) + "++";
        }

        public override void Visit(Visitor v)
        {
            v.VisitDecrement(this);
        }
    }
}
