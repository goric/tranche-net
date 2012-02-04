using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractSyntaxTree
{
    public class ExpressionList : Expression
    {
        public bool IsEmpty { get; set; }
        public Expression Head { get; set; }
        public ExpressionList Tail { get; set; }
        public int Length { get; set; }

        public ExpressionList ()
        {
            IsEmpty = true;
            Length = 0;
        }
        public ExpressionList(Expression exp, ExpressionList tail)
        {
            Head = exp;
            Tail = tail;
            Length = Tail.Length + 1;
        }

        public override string Print (int depth)
        {
            if (IsEmpty)
                return "";

            if (Tail.IsEmpty)
                return Head.Print(depth).ToString();
            else
                return Head.Print(depth) + "," + Tail.Print(depth);
        }

        public override void Visit (Visitor v)
        {
            v.VisitExprList(this);
        }
    }
}
