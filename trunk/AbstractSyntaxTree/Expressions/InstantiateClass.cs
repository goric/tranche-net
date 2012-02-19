using System;
using SemanticAnalysis;

namespace AbstractSyntaxTree
{
    public class InstantiateClass : Expression
    {
        public String ClassName { get; set; }
        public ExpressionList Actuals { get; set; }
        public Descriptor Descriptor { get; set; }
        public Descriptor ClassDescriptor { get; set; }

        public InstantiateClass(String className, ExpressionList actuals)
        {
            ClassName = className;
            Actuals = actuals;
        }

        public override String Print(int depth)
        {
            return " new " + ClassName + "(" + Actuals.Print(depth) + ")";
        }

        public override void Visit(Visitor v)
        {
            v.VisitInstantiateClass(this);
        }
    }
}
