using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractSyntaxTree
{
    public class RealLiteral : Expression
    {
        public double Value { get; set; }

        public RealLiteral (double val)
        {
            Value = val;
        }
        
        public override String Print(int depth)
        {
            return Value.ToString();
        }

        public override void Visit (Visitor v)
        {
            v.VisitRealLiteral(this);
        }
    }
}
