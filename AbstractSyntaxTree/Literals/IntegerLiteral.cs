using System;
using System.Globalization;

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
            return Value.ToString(CultureInfo.InvariantCulture);
        }

        public override void Visit (Visitor v)
        {
            v.VisitIntegerLiteral(this);
        }
    }
}
