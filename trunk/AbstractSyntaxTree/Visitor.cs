
namespace AbstractSyntaxTree
{
    public abstract class Visitor
    {
        public virtual void VisitProgram (Prog n) { }
        public virtual void VisitDeclarationClass(DeclarationClass n) { }

        #region Expressions

        public virtual void VisitStringLiteral (StringLiteral n) { }
        public virtual void VisitIntegerLiteral (IntegerLiteral n) { }
        public virtual void VisitRealLiteral (RealLiteral n) { }
        public virtual void VisitBooleanLiteral (BooleanLiteral n) { }
        public virtual void VisitIdentifier (Identifier n) { }
        public virtual void VisitEqual (Equal n) { }
        public virtual void VisitNotEqual (NotEqual n) { }
        public virtual void VisitSmallerEqual (SmallerEqual n) { }
        public virtual void VisitGreaterEqual (GreaterEqual n) { }
        public virtual void VisitGreater (Greater n) { }
        public virtual void VisitSmaller (Smaller n) { }
        public virtual void VisitDereferenceField(DereferenceField n) { }
        public virtual void VisitInstantiateClass(InstantiateClass n) { }
        public virtual void VisitSpecialFunction(SpecialFunction n) { }
        public virtual void VisitConcat(Concat n) { }

        public virtual void VisitExprList (ExpressionList n)
        {
            if (!n.IsEmpty)
            {
                n.Head.Visit(this);
                n.Tail.Visit(this);
            }
        }

        #region arithmetic

        public virtual void VisitExp(Exp n){}
        public virtual void VisitMod(Mod n) { }
        public virtual void VisitDivide(Divide n) { }
        public virtual void VisitTimes(Times n) { }
        public virtual void VisitMinus(Minus n) { }
        public virtual void VisitPlus(Plus n) { }
        public virtual void VisitIncrement(Increment n) { }
        public virtual void VisitDecrement(Decrement n) { }

        #endregion

        #endregion

        public virtual void VisitInternalRuleList (InternalTypes.InternalRuleList n) { }
        public virtual void VisitInternalRuleListOr (InternalTypes.InternalRuleListOr n) { }
        public virtual void VisitInternalRuleListAnd (InternalTypes.InternalRuleListAnd n) { }

        #region Statements

        public virtual void VisitStatementExpression (StatementExpression n) { }
        public virtual void VisitInvoke (Invoke n) { }
        public virtual void VisitAssign(Assign n) { }
        public virtual void VisitLoop(Loop n) { }
        public virtual void VisitQualifier(Qualifier n) { }
        public virtual void VisitCons(Cons n) { }
        public virtual void VisitFilter(Filter n) { }
        public virtual void VisitAggregate(Aggregate n) { }
        public virtual void VisitRuleType(RuleType n) { }

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

        public virtual void VisitSet(Set n) { }
        public virtual void VisitTimeSeries(TimeSeries n) { }

        #endregion
    }
}
