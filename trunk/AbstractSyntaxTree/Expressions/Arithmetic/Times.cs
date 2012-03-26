
namespace AbstractSyntaxTree
{
    public class Times : BinaryExpression
    {
        public bool IsSetWise { get; set; }

        public Times(Expression left, Expression right) : this(left, false, right) { }
        public Times(Expression left, bool setWise, Expression right) : base(left, right)
        {
            IsSetWise = setWise;
        }

        public override string Print(int depth)
        {
            return Left.Print(depth) + " * " + Right.Print(depth);
        }

        public override void Visit(Visitor v)
        {
            v.VisitTimes(this);
        }
    }
}
