using System;

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
