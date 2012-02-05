using System;

namespace AbstractSyntaxTree
{
    public class StatementExpression : Statement
    {
        public Expression Expression { get; set; }

        public StatementExpression (Expression exp)
        {
            Expression = exp;
        }

        public override String Print (int depth)
        {
            return Expression.Print(depth);
        }

        public override void Visit (Visitor v)
        {
            v.VisitStatementExpression(this);
        }
    }
}
