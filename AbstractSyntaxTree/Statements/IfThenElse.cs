using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractSyntaxTree
{
    public class IfThenElse : IfThen
    {
        public Block Else { get; set; }

        public IfThenElse (Expression condition, Statement then, Statement elseStatement)
            : base(condition, then)
        {
            Else = elseStatement.WrapInBlock();
            Else.IsBranch = true;
        }
        
        public override String Print(int depth)
        {
            return base.Print(depth) + NewLine(depth) + "else " + NewLine(depth + 1) + Else.Print(depth + 1);
        }

        public override void Visit (Visitor v)
        {
            v.VisitIfThenElse(this);
        }
    }
}
