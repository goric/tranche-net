
namespace AbstractSyntaxTree
{
    public class Qualifier : Expression
    {
        public string Type { get; set; }
        public Expression Expression { get; set; }

        public Qualifier(string type, Expression e)
        {
            Type = type;
            Expression = e;
        }

        public override string Print(int depth)
        {
            return "(" + Type + ")" + Expression;
        }

        public override void Visit(Visitor v)
        {
            v.VisitQualifier(this);
        }
    }
}
