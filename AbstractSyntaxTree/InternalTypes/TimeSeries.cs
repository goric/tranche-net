
namespace AbstractSyntaxTree
{
    public class TimeSeries : Statement
    {
        public Identifier First { get; set; }
        public Identifier Second { get; set; }

        public TimeSeries(Identifier first, Identifier second)
        {
            First = first;
            Second = second;
        }

        public override string Print(int depth)
        {
            return "<" + First.Print(depth) + "," + Second.Print(depth) + ">";
        }

        public override void Visit(Visitor v)
        {
            v.VisitTimeSeries(this);
        }
    }
}
