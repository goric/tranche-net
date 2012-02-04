using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractSyntaxTree
{
    public class IfThen : Statement
    {
        public Expression Condition { get; set; }
        public Block Then { get; set; }

        public IfThen (Expression condition, Statement then)
        {
            Condition = condition;

            Then = then.WrapInBlock();
            Then.IsBranch = true;
        }
        
        public override String Print(int depth)
        {
            return "if (" + Condition.Print(depth) + ")" + NewLine(depth + 1) + Then.Print(depth + 1);
        }

        public override void Visit (Visitor v)
        {
            v.VisitIfThen(this);
        }
    }
}
