
namespace AbstractSyntaxTree.InternalTypes
{
    public class Deal : DeclarationClass
    {
        public Deal () : this(null) { }
        public Deal (StatementList stmt) : base("Deal", stmt) { }
        
        public override void Visit (Visitor v)
        {
            v.VisitDeal(this);
        }
    }
}
