using System;
using System.Linq;

using AbstractSyntaxTree;
using AbstractSyntaxTree.InternalTypes;
using SemanticAnalysis;

namespace tc
{
    /// <summary>
    /// This pass determines the types for fields and assigns it back to the 
    /// syntax tree node for later use in the ILGen.
    /// </summary>
    public class SecondPass : Visitor
    {
        private InternalType _lastSeenType;
        private readonly Node _root;
        private readonly ScopeManager _mgr;
        private TypeClass _currentClass;
        private int _itemIncrementer;

        public SecondPass(Node n, ScopeManager mgr)
        {
            _mgr = mgr;
            _root = n;
        }
        public void Run()
        {
            _root.Visit(this);
        }

        public override void VisitProgram(Prog n)
        {
            n.Settings.Visit(this);
            n.Deal.Visit(this);
            n.Collateral.Visit(this);
            n.Securities.Visit(this);
            n.CreditPaymentRules.Visit(this);
            n.Simulation.Visit(this);
        }
        public override void VisitSettings(Settings n) { VisitDeclarationClass(n); }
        public override void VisitDeal(Deal n) { VisitDeclarationClass(n); }
        public override void VisitCollateral(Collateral n) { VisitDeclarationClass(n); }
        public override void VisitSecurities(Securities n) { VisitDeclarationClass(n); }
        public override void VisitCreditPaymentRules(CreditPaymentRules n) { VisitDeclarationClass(n); }
        public override void VisitSimulation(Simulation n) { VisitDeclarationClass(n); }

        public override void VisitDeclarationClass(DeclarationClass n)
        {
            _itemIncrementer = 0;
            _currentClass = new TypeClass(n.Name);
            var classScope = _mgr.PushScope(string.Format("class {0}", _currentClass.ClassName));

            _currentClass.Descriptor = (ClassDescriptor)_mgr.Find(n.Name, p => p is ClassDescriptor);

            CheckSubTree(n.Statements);
            n.Type = _currentClass;

            _currentClass.Descriptor.Scope = _currentClass.Scope = classScope;
            AddCtorIfNone(classScope, n.Name);
            _mgr.PopScope();
        }

        public override void VisitCollateralItem(CollateralItem n)
        {
            _mgr.PushScope(string.Format("CollateralItem{0}", _itemIncrementer++));
            n.Statements.Visit(this);
            _mgr.PopScope();

            if (!n.Tail.IsEmpty)
                n.Tail.Visit(this);
        }

        public override void VisitStatementList(StatementList n)
        {
            if (n.IsEmpty) return;

            n.Head.Visit(this);
            n.Tail.Visit(this);
        }

        public override void VisitAssign(Assign n)
        {
            var declFieldType = CheckSubTree(n.Expr);
            n.InternalType = declFieldType;

            var desc = _mgr.AddMember(n.LValue.Id, declFieldType, _currentClass);
            n.Descriptor = desc;
        }

        public override void VisitStringLiteral(StringLiteral n)
        {
            n.InternalType = _lastSeenType = new TypeString();
        }
        public override void VisitIntegerLiteral(IntegerLiteral n)
        {
            n.InternalType = _lastSeenType = new TypeInteger();
        }
        public override void VisitBooleanLiteral(BooleanLiteral n)
        {
            n.InternalType = _lastSeenType = new TypeBoolean();
        }
        public override void VisitRealLiteral(RealLiteral n)
        {
            n.InternalType = _lastSeenType = new TypeReal();
        }

        public InternalType CheckSubTree(Node root)
        {
            _lastSeenType = null;
            root.Visit(this);
            return _lastSeenType;
        }

        private void AddCtorIfNone(Scope classScope, string name)
        {
            var ctor = _currentClass.Descriptor.Methods.SingleOrDefault(p => p.Name.Equals(_currentClass.ClassName, StringComparison.OrdinalIgnoreCase));
            if (ctor == null)
            {
                var func = new TypeFunction(name) { ReturnType = new TypeVoid(), IsConstructor = true, Scope = classScope };
                _mgr.AddMethod(name, func, _currentClass);
            }
        }
    }
}
