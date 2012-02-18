using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
