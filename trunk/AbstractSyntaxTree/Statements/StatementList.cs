using System;

namespace AbstractSyntaxTree
{
    public class StatementList : Statement
    {
        public bool IsEmpty { get; set; }
        public Statement Head { get; set; }
        public StatementList Tail { get; set; }

        public StatementList ()
        {
            IsEmpty = true;
        }
        public StatementList (Statement statement, StatementList tail)
        {
            IsEmpty = false;
            Head = statement;
            Tail = tail;
        }

        public override String Print (int depth)
        {
            if (IsEmpty)
                return "";

            if (Tail == null)
                return Head.Print(depth) + ";";
            
            return Head.Print(depth) + ";" + NewLine(depth) + Tail.Print(depth);
        }

        public override void Visit (Visitor v)
        {
            v.VisitStatementList(this);
        }
    }
}
