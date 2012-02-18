using AbstractSyntaxTree;
using AbstractSyntaxTree.InternalTypes;

namespace tc
{
    /// <summary>
    /// Struct used in the Parser generator as the TValue input for the ShiftReduceParser base class.
    /// This allows to strongly type both the terminals and the non-terminals in the grammar input file
    /// so each semantic action defined will produce an ASTNode of the proper type.
    /// </summary>
    internal struct SemanticValue
    {
        public Token Token { get; set; }

        public Prog Prog { get; set; }
        public StatementList StatementList { get; set; }
        public Statement Statement { get; set; }
        public ExpressionList ExpressionList { get; set; }
        public Expression Expression { get; set; }
        public Bond Bond { get; set; }
        public Collateral Collateral { get; set; }
        public CollateralItem CollateralItem { get; set; }
        public Deal Deal { get; set; }
        public CreditPaymentRules CreditPaymentRules { get; set; }
        public InterestRules InterestRules { get; set; }
        public PrincipalRules PrincipalRules { get; set; }
        public InternalRuleList InternalRuleList { get; set; }
        public Securities Securities { get; set; }
        public Settings Settings { get; set; }
        public Simulation Simulation { get; set; }
        public DeclarationClass DeclarationClass { get; set; }
    }
}
