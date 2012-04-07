
namespace AbstractSyntaxTree
{
    public class Concat : Expression
    {
        public Expression Left { get; set; }
        public Expression Right { get; set; }

        public Concat(Expression first, Expression second)
        {
            Left = first;
            Right = second;
        }

        public override string Print(int depth)
        {
            return Left.Print(depth) + " & " + Right.Print(depth);
        }

        public override void Visit(Visitor v)
        {
            v.VisitConcat(this);
        }
    }
}
