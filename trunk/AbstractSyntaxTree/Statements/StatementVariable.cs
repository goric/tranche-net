using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using QUT.Gppg;

namespace AbstractSyntaxTree
{
    public class StatementVariable : Statement
    {
        public string ID { get; set; }
        public bool HasValue { get; set; }
        public Expression InitialValue { get; set; }

        public StatementVariable (LexLocation loc, string id)
        {
            HasValue = false;
            Location = loc;
            ID = id;
        }

        public StatementVariable (LexLocation loc, string id, Expression value)
        {
            HasValue = true;
            Location = loc;
            ID = id;
            InitialValue = value;
        }

        public override String Print (int depth)
        {
            if (!HasValue)
                return ID;

            return ID + " = " + InitialValue.Print(depth);
        }

        public override void Visit (Visitor v)
        {
            v.VisitStatementVariable(this);
        }
    }
}
