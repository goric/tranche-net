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
        public Qualifier Qualifier { get; set; }
        public Statement Statement { get; set; }

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
            Qualifier = qual;
        }

        public Assign(LValue lval, Statement s)
        {
            lval.IsLeftHandSide = true;
            LValue = lval;
            Statement = s;
        }

        public override String Print (int depth)
        {
            var str = LValue.Print(depth) + " = ";
            if (Qualifier != null)
                str += Qualifier.Print(depth);
            else
                str += (Expr == null ? Statement.Print(depth) : Expr.Print(depth));

            return str;
        }

        public override void Visit (Visitor v)
        {
            v.VisitAssign(this);
        }
    }
}
