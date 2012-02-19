using System;

using SemanticAnalysis;

namespace AbstractSyntaxTree
{
    public class Assign : Statement
    {
        public LValue LValue { get; private set; }
        public string Name { get { return LValue.Id; } }
        public Expression Expr { get; private set; }
        public Descriptor Descriptor { get; set; }

        public Assign(LValue lval, Expression exp)
        {
            lval.IsLeftHandSide = true;
            LValue = lval;
            Expr = exp;
        }

        public override String Print (int depth)
        {
            return LValue.Print(depth) + " = " + Expr.Print(depth);
        }

        public override void Visit (Visitor v)
        {
            v.VisitAssign(this);
        }
    }
}
