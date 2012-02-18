
using SemanticAnalysis;

namespace AbstractSyntaxTree.InternalTypes
{
    public class Collateral : DeclarationClass
    {
        public Collateral (StatementList tail) : base("Collateral", tail) { }
        
        public override void Visit (Visitor v)
        {
            v.VisitCollateral(this);
        }
    }
}
