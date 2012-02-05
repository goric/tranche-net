namespace AbstractSyntaxTree
{
    public class BooleanLiteral : Expression
    {
        public bool Val { get; protected set; }

        public BooleanLiteral (bool value)
        {
            Val = value;
        }

        public override void Visit (Visitor v)
        {
            v.VisitBooleanLiteral(this);
        }

        public override string Print (int depth)
        {
            return Val.ToString();
        }
    }
}
