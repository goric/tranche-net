
namespace AbstractSyntaxTree
{
    public class Aggregate : Statement
    {
        public Identifier Identifier { get; set; }
        public Expression Expression { get; set; }

        public Aggregate(Identifier ident, Expression e)
        {
            Identifier = ident;
            Expression = e;
        }

        public override string Print(int depth)
        {
            return "aggregate " + Identifier.Print(depth) + " " + Expression.Print(depth);
        }

        public override void Visit(Visitor v)
        {
            v.VisitAggregate(this);
        }
    }
}
