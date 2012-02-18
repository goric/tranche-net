
namespace AbstractSyntaxTree
{
    public abstract class Visitor
    {
        public bool DebugMode { get; set; }
        public virtual void VisitNode (Node n) { }

        public virtual void VisitProgram (Prog n) { }

        public virtual void VisitDeclaration (Declaration n) { }
        public virtual void VisitDeclarationMethod (DeclarationMethod n) { }
        public virtual void VisitDeclarationClass(DeclarationClass n) { }

        #region Expressions

        public virtual void VisitExpr (Expression n) { }
        public virtual void VisitStringLiteral (StringLiteral n) { }
        public virtual void VisitIntegerLiteral (IntegerLiteral n) { }
        public virtual void VisitRealLiteral (RealLiteral n) { }
        public virtual void VisitBooleanLiteral (BooleanLiteral n) { }
        public virtual void VisitIdentifier (Identifier n) { }
        public virtual void VisitBinaryExpression (BinaryExpression n) { }
        public virtual void VisitEqual (Equal n) { }
        public virtual void VisitNotEqual (NotEqual n) { }
        public virtual void VisitSmallerEqual (SmallerEqual n) { }
        public virtual void VisitGreaterEqual (GreaterEqual n) { }
        public virtual void VisitGreater (Greater n) { }
        public virtual void VisitSmaller (Smaller n) { }
        public virtual void VisitExprList (ExpressionList n)
        {
            if (!n.IsEmpty)
            {
                n.Head.Visit(this);
                n.Tail.Visit(this);
            }
        }

        #endregion

        public virtual void VisitInternalRuleList (InternalTypes.InternalRuleList n) { }
        public virtual void VisitInternalRuleListOr (InternalTypes.InternalRuleListOr n) { }
        public virtual void VisitInternalRuleListAnd (InternalTypes.InternalRuleListAnd n) { }

        #region Statements

        public virtual void VisitStatement (Statement n) { }
        public virtual void VisitStatementExpression (StatementExpression n) { }
        public virtual void VisitBlock (Block n) { }
        public virtual void VisitStatementVariable (StatementVariable n) { }
        public virtual void VisitIfThen (IfThen n) { }
        public virtual void VisitIfThenElse (IfThenElse n) { }
        public virtual void VisitInvoke (Invoke n) { }

        public virtual void VisitStatementList (StatementList n)
        {
            if (!n.IsEmpty)
            {
                n.Head.Visit(this);
                n.Tail.Visit(this);
            }
        }

        #endregion

        #region Internal Types

        public virtual void VisitCollateral (InternalTypes.Collateral n) { }
        public virtual void VisitDeal (InternalTypes.Deal n) { }
        public virtual void VisitSettings (InternalTypes.Settings n) { }
        public virtual void VisitSecurities (InternalTypes.Securities n) { }
        public virtual void VisitCreditPaymentRules (InternalTypes.CreditPaymentRules n) { }
        public virtual void VisitInterestRules (InternalTypes.InterestRules n) { }
        public virtual void VisitPrincipalRules (InternalTypes.PrincipalRules n) { }
        public virtual void VisitSimulation (InternalTypes.Simulation n) { }
        public virtual void VisitCollateralItem (InternalTypes.CollateralItem n) { }
        public virtual void VisitBond (InternalTypes.Bond n) { }

        #endregion
    }
}
