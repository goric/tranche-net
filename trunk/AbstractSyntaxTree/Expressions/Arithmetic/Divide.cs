
namespace AbstractSyntaxTree
{
    public class Divide : BinaryExpression
    {
        public bool IsSetWise { get; set; }

        public Divide(Expression left, Expression right) : this(left, false, right) { }
        public Divide(Expression left, bool setWise, Expression right) : base(left, right)
        {
            IsSetWise = setWise;
        }

        public override string Print(int depth)
        {
            return Left.Print(depth) + " / " + Right.Print(depth);
        }

        public override void Visit(Visitor v)
        {
            v.VisitDivide(this);
        }
    }
}
