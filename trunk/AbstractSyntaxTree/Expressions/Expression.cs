using System;

//using SemanticAnalysis;

namespace AbstractSyntaxTree
{
    public class Expression : Node
    {
        //protected CFlatType Type { get; set; }

        public override String Print (int depth)
        {
            return "";
        }

        public override void Visit (Visitor v)
        {
            v.VisitExpr(this);
        }
    }
}
