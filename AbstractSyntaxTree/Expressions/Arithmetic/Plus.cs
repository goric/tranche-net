
namespace AbstractSyntaxTree
{
    public class Plus : BinaryExpression
    {
        public bool IsSetWise { get; set; }

        public Plus(Expression left, Expression right) : this(left, false, right) { }
        public Plus(Expression left, bool setWise, Expression right) : base(left, right)
        {
            IsSetWise = setWise;
        }

        public override string Print(int depth)
        {
            return Left.Print(depth) + " + " + Right.Print(depth);
        }

        public override void Visit(Visitor v)
        {
            v.VisitPlus(this);
        }
    }
}
