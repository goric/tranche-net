
namespace AbstractSyntaxTree
{
    public class Cons : Statement
    {
        public Identifier Item { get; set; }
        public Expression Expression { get; set; }

        public Cons(Identifier i, Expression e)
        {
            Item = i;
            Expression = e;
        }

        public override string Print(int depth)
        {
            return Item.Print(depth) + " :: " + Expression.Print(depth);
        }

        public override void Visit(Visitor v)
        {
            v.VisitCons(this);
        }
    }
}
