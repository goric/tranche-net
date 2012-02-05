
namespace AbstractSyntaxTree
{
    public class NotEqual : BinaryExpression
    {
        public NotEqual (Expression l, Expression r) : base(l, r) {}

        public override string Print (int depth)
        {
            return Left.Print(depth) + " != " + Right.Print(depth);
        }

        public override void Visit (Visitor v)
        {
            v.VisitNotEqual(this);
        }
    }
}
