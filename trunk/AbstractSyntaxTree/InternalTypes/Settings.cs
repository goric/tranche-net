
using SemanticAnalysis;

namespace AbstractSyntaxTree.InternalTypes
{
    public class Settings : DeclarationClass
    {
        public Settings () : this(null) { }
        public Settings (StatementList stmt) : base("Settings", stmt)
        {
        }
        
        public override void Visit (Visitor v)
        {
            v.VisitSettings(this);
        }
    }
}
