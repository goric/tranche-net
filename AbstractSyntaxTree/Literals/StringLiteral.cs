using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractSyntaxTree
{
    public class StringLiteral : Expression
    {
        public string Value { get; set; }

        public StringLiteral (string val)
        {
            Value = val;
        }
        
        public override String Print(int depth)
        {
            return Value;
        }

        public override void Visit (Visitor v)
        {
            v.VisitStringLiteral(this);
        }
    }
}
