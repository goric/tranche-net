
namespace AbstractSyntaxTree
{
    public class Filter : Expression
    {
        public Identifier SetName { get; set; }
        public Identifier RuleInput { get; set; }
        public string Operation { get; set; }

        public Identifier TempVar { get; set; }
        public Expression Expression { get; set; }

        public Filter(Identifier set, Identifier input, string operation)
        {
            SetName = set;
            RuleInput = input;
            Operation = operation;
        }
        public Filter(Identifier ident, Identifier tmpVar, Expression e)
        {
            SetName = ident;
            TempVar = tmpVar;
            Expression = e;
        }

        public override string Print(int depth)
        {
            if (!string.IsNullOrEmpty(Operation))
                return "filter " + SetName.Print(depth) + " " + RuleInput.Print(depth) + " " + Operation;
            return "filter " + SetName.Print(depth) + " " + TempVar.Print(depth) + " " + Expression.Print(depth);
        }

        public override void Visit(Visitor v)
        {
            v.VisitFilter(this);
        }
    }
}
