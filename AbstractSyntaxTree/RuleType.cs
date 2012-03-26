
namespace AbstractSyntaxTree
{
    public class RuleType :Statement
    {
        public Expression Expression { get; set; }

        public RuleType(Expression e)
        {
            Expression = e;
        }

        public override string Print(int depth)
        {
            return "|" + Expression.Print(depth) + "|";
        }

        public override void Visit(Visitor v)
        {
            v.VisitRuleType(this);
        }
    }
}
