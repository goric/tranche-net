using SemanticAnalysis;

namespace AbstractSyntaxTree.InternalTypes
{
    public class PrincipalRules : StatementList
    {
        public InternalRuleList Statements { get; set; }

        public PrincipalRules () { }
        public PrincipalRules(InternalRuleList stmt)
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
