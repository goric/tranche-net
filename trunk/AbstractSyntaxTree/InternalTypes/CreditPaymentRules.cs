
namespace AbstractSyntaxTree.InternalTypes
{
    public class CreditPaymentRules : DeclarationClass
    {
        public CreditPaymentRules () : this(null) { }
        public CreditPaymentRules (StatementList tail) : base("CreditPaymentRules", tail) { }
        
        public override void Visit (Visitor v)
        {
            v.VisitCreditPaymentRules(this);
        }
    }
}
