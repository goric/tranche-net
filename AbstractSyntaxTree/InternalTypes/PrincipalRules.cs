
namespace AbstractSyntaxTree.InternalTypes
{
    public class PrincipalRules : StatementList
    {
        public Expression Statements { get; set; }

        public PrincipalRules () { }
        public PrincipalRules (Expression stmt)
        {
            Statements = stmt;
        }
        
        public override void Visit (Visitor v)
        {
            v.VisitPrincipalRules(this);
        }

        public override string Print (int depth)
        {
            if (Statements == null)
                return string.Empty;
            return "Principal {" + NewLine(depth + 1) + Statements.Print(depth + 1) + NewLine(depth) + "}" + NewLine(depth);
        }
    }
}
