
namespace AbstractSyntaxTree
{
    public class BinaryExpression : Expression
    {
        public Expression Left { get; set; }
        public Expression Right { get; set; }

        protected BinaryExpression(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override void Visit (Visitor v)
        {
        }
    }
}
