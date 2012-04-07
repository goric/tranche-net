using System;
using System.Linq;

using AbstractSyntaxTree;
using AbstractSyntaxTree.InternalTypes;
using QUT.Gppg;
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
            
            if(n.Statements != null)
                CheckSubTree(n.Statements);
            
            n.Type = _currentClass;

            _currentClass.Descriptor.Scope = _currentClass.Scope = classScope;
            AddCtorIfNone(classScope, n.Name);
            _mgr.PopScope();
        }

        public override void VisitCollateralItem(CollateralItem n)
        {
            _mgr.PushScope(string.Format("CollateralItem{0}", _itemIncrementer++));
            
            if(n.Statements != null)
                n.Statements.Visit(this);
            
            _mgr.PopScope();

            if (n.Tail != null && !n.Tail.IsEmpty)
                n.Tail.Visit(this);
        }

        public override void VisitBond(Bond n)
        {
            _mgr.PushScope(string.Format("Bond{0}", _itemIncrementer++));

            if (n.Statements != null)
                n.Statements.Visit(this);

            _mgr.PopScope();

            if (n.Tail != null && !n.Tail.IsEmpty)
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
            InternalType declFieldType;
            if(n.Expr != null)
                declFieldType = CheckSubTree(n.Expr);
            else if(n.Statement != null)
                declFieldType = CheckSubTree(n.Expr);
            else
                declFieldType = CheckSubTree(n.Qualifier);

            n.InternalType = declFieldType;

            var desc = _mgr.AddMember(n.LValue.Id, declFieldType, _currentClass);
            n.Descriptor = desc;
        }

        public override void VisitIdentifier(Identifier n)
        {
            if (_mgr.HasSymbol(n.Id))
            {
                var d = _mgr.GetType(n.Id);
                n.Descriptor = d;
                n.InternalType = d.Type;

                _lastSeenType = d.Type;
            }
            else
            {
                ReportError(n.Location, "Identifier '{0}' has not been declared.", n.Id);
            }
        }

        public override void VisitDereferenceField(DereferenceField n)
        {
            //lvalue.identifier
            var lhs = CheckSubTree(n.Object);

            if(!(lhs is TypeClass))
            {
                var members = lhs.CilType.GetMembers();
                var thisProperty = members.FirstOrDefault(p => p.Name.Equals(n.Field, StringComparison.OrdinalIgnoreCase));

                if (thisProperty == null)
                    throw new Exception(string.Format("Attribute {0} not available on type {1} (object {2})", n.Field,
                                                      lhs.CilType, n.Object));

                switch(lhs.CilType.GetProperty("Days").PropertyType.Name)
                {
                    case "Int32":
                        n.InternalType = _lastSeenType = new TypeInteger();
                        break;
                    case "DateTime":
                        n.InternalType = _lastSeenType = new TypeDate();
                        break;
                    case "String":
                        n.InternalType = _lastSeenType = new TypeString();
                        break;
                    case "Double":
                        n.InternalType = _lastSeenType = new TypeReal();
                        break;
                    case "Boolean":
                        n.InternalType = _lastSeenType = new TypeBoolean();
                        break;
                }
            }
        }

        public override void VisitQualifier(Qualifier n)
        {
            n.Expression.Visit(this);
            switch(n.Type.ToLower())
            {
                case "date":
                    _lastSeenType = n.InternalType = new TypeDate();
                    break;
                case "real":
                    _lastSeenType = n.InternalType = new TypeReal();
                    break;
                case "integer":
                    _lastSeenType = n.InternalType = new TypeInteger();
                    break;
                case "string":
                    _lastSeenType = n.InternalType = new TypeString();
                    break;
                case "boolean":
                    _lastSeenType = n.InternalType = new TypeBoolean();
                    break;
                default:
                    throw new Exception("Unknown type qualifier: (" + n.Type + ")");
            }
        }

        public override void VisitPlus(Plus n)
        {
            VisitBinaryArithmetic(n);
        }
        public override void VisitMinus(Minus n)
        {
            VisitBinaryArithmetic(n);
        }
        public override void VisitTimes(Times n)
        {
            VisitBinaryArithmetic(n);
        }
        public override void VisitDivide(Divide n)
        {
            VisitBinaryArithmetic(n);
        }
        public override void VisitExp(Exp n)
        {
            VisitBinaryArithmetic(n);
        }
        public override void VisitMod(Mod n)
        {
            VisitBinaryArithmetic(n);
        }
        public override void VisitIncrement(Increment n)
        {
            _lastSeenType = n.InternalType = TypeCheckIncrementDecrement(n.Expression, "++", n.Location);
        }
        public override void VisitDecrement(Decrement n)
        {
            _lastSeenType = n.InternalType = TypeCheckIncrementDecrement(n.Expression, "--", n.Location);
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

        private InternalType CheckSubTree(Node root)
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

        private static InternalType Supertype(InternalType t1, InternalType t2)
        {
            return t1.IsSupertype(t2) ? t2 : t2.IsSupertype(t1) ? t1 : null;
        }

        private static string TypeToFriendlyName(InternalType t)
        {
            return t.IsClass ? ((TypeClass)t).ClassName : t.ToString();
        }

        private static void ReportError(LexLocation loc, string msg, params string[] formatArgs)
        {
            var formattedMsg = String.Format(msg, formatArgs);
            var location = string.Format(" line {0} column {1}", loc.StartLine, loc.StartColumn);
            throw new Exception(String.Format(
                "{0}{1}  at {2}",
                formattedMsg,
                Environment.NewLine,
                (loc != null) ? location : "unknown")
                );
        }

        private void VisitBinaryArithmetic(BinaryExpression n)
        {
            var lhs = CheckSubTree(n.Left);
            var rhs = CheckSubTree(n.Right);

            if (lhs.IsNumeric && rhs.IsNumeric)
            {
                _lastSeenType = n.InternalType = Supertype(lhs, rhs);
            }
            else if(lhs.CilType == typeof(DateTime) && rhs.CilType == typeof(DateTime))
            {
                _lastSeenType = n.InternalType = new TypeSpan();
            }
            else
            {
                ReportError(n.Location, "Invalid operands for operation {0}. Got types '{1}' and '{2}'.", n.GetType().ToString(), TypeToFriendlyName(lhs), TypeToFriendlyName(rhs));
            }
        }
        private InternalType TypeCheckIncrementDecrement(Expression expr, string operatorName, LexLocation loc)
        {
            if (!(expr is Identifier))
            {
                ReportError(loc, "The {0} operator requires an instance of a numeric datatype", operatorName);
                throw new Exception(string.Format("The {0} operator requires an instance of a numeric datatype", operatorName));
            }

            var identifier = (Identifier)expr;
            identifier.IsLeftHandSide = true;

            var t = CheckSubTree(identifier);
            if (t.IsNumeric)
                return t;

            ReportError(loc, "The {0} operator requires an instance of a numeric datatype", operatorName);
            throw new Exception(string.Format("The {0} operator requires an instance of a numeric datatype", operatorName));
        }
    }
}
