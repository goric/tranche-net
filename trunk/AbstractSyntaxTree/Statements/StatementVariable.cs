using System;

using QUT.Gppg;

namespace AbstractSyntaxTree
{
    public class StatementVariable : Statement
    {
        public string Id { get; set; }
        public bool HasValue { get; set; }
        public Expression InitialValue { get; set; }

        public StatementVariable (LexLocation loc, string id)
        {
            HasValue = false;
            Location = loc;
            Id = id;
        }

        public StatementVariable (LexLocation loc, string id, Expression value)
        {
            HasValue = true;
            Location = loc;
            Id = id;
            InitialValue = value;
        }

        public override String Print (int depth)
        {
            if (!HasValue)
                return Id;

            return Id + " = " + InitialValue.Print(depth);
        }

        public override void Visit (Visitor v)
        {
            v.VisitStatementVariable(this);
        }
    }
}
