
namespace AbstractSyntaxTree
{
    public class Minus : BinaryExpression
    {
        public bool IsSetWise { get; set; }

        public Minus(Expression left, Expression right) : this(left, false, right) { }
        public Minus(Expression left, bool setWise, Expression right) : base(left, right)
        {
            IsSetWise = setWise;
        }

        public override string Print(int depth)
        {
            return Left.Print(depth) + " - " + Right.Print(depth);
        }

        public override void Visit(Visitor v)
        {
            v.VisitMinus(this);
        }
    }
}
