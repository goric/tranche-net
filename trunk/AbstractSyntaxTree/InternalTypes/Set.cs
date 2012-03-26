
namespace AbstractSyntaxTree
{
    public class Set : Statement
    {
        public StatementList InitialStatements { get; set; }
        public Set(StatementList initial)
        {
            InitialStatements = initial;
        }

        public override string Print(int depth)
        {
            return "[ " + InitialStatements.Print(depth) + "]";
        }

        public override void Visit(Visitor v)
        {
            v.VisitSet(this);
        }
    }
}
