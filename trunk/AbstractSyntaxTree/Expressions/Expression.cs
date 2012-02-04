using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using SemanticAnalysis;

namespace AbstractSyntaxTree
{
    public class Expression : Node
    {
        //protected CFlatType Type { get; set; }

        public Expression () { }

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
