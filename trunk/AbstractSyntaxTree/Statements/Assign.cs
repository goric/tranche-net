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
        public string Qualifier { get; set; }

        public Assign(LValue lval, Expression exp)
        {
            lval.IsLeftHandSide = true;
            LValue = lval;
            Expr = exp;
        }

        public Assign(LValue lval, Qualifier qual)
        {
            lval.IsLeftHandSide = true;
            LValue = lval;
            Qualifier = qual.Type;
            Expr = qual.Expression;
        }

        public override String Print (int depth)
        {
            return LValue.Print(depth) + " = " +
                   (string.IsNullOrEmpty(Qualifier) ? string.Empty : "(" + Qualifier + ")")
                   + Expr.Print(depth);
        }

        public override void Visit (Visitor v)
        {
            v.VisitAssign(this);
        }
    }
}
