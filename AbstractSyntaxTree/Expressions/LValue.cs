using System;

using SemanticAnalysis;

namespace AbstractSyntaxTree
{
    public class LValue : Expression
    {
        public String Id { get; set; }

        public Descriptor Descriptor { get; set; }

        public bool IsLeftHandSide { get; set; }
    }
}
