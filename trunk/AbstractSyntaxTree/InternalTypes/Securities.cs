
using SemanticAnalysis;

namespace AbstractSyntaxTree.InternalTypes
{
    public class Securities : DeclarationClass
    {
        public Securities (StatementList stmt) : base("Securities", stmt) { }
        
        public override void Visit (Visitor v)
        {
            v.VisitSecurities(this);
        }
    }
}
