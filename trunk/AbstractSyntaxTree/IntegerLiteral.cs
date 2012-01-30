using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractSyntaxTree
{
    public class IntegerLiteral : Expression
    {
        public int Value { get; set; }

        public IntegerLiteral (int val)
        {
            Value = val;
        }
        
        public override String Print(int depth)
        {
            return Value.ToString();
        }

        public override void Visit (Visitor v)
        {
            v.VisitIntegerLiteral(this);
        }
    }
}
