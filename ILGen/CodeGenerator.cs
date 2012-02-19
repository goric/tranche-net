using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

using AbstractSyntaxTree;
using AbstractSyntaxTree.InternalTypes;
using SemanticAnalysis;

namespace ILGen
{
    internal enum InternalTypes
    {
        Settings,
        Deal,
        Collateral,
        Securities,
        CreditPaymentRules
    }
    public class CodeGenerator : Visitor
    {
        protected AssemblyBuilder _assemblyBuilder;
        protected ModuleBuilder _moduleBuilder;
        protected ILGenerator _gen;
        protected TypeManager _typeManager;
        protected TypeBuilderInfo _currentTypeBuilder;
        protected BuilderInfo _currentMethodBuilder;
        protected Type _lastWalkedType;

        private readonly string _assemblyName;
        private string _currentType;
        private Action<ILGenerator> _assignmentCallback;

        private const string ENTRY_POINT = "Simulation";


        public CodeGenerator (string assemblyName)
        {
            _assemblyName = assemblyName;
            
            InitAssembly();
        }

        private void InitAssembly ()
        {
            var asmName = new AssemblyName(_assemblyName);
            _assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.RunAndSave);

            //Create Module
            var exeName = _assemblyName + ".exe";
            _moduleBuilder = _assemblyBuilder.DefineDynamicModule(exeName, exeName);

            _typeManager = new TypeManager(_moduleBuilder);
        }

        public void Generate (Node n)
        {
            n.Visit(this);
            _typeManager.CreateAllTypes();
        }

        public void WriteAssembly ()
        {
            _assemblyBuilder.Save(_assemblyName + ".exe");
        }

        #region Internal Types

        public override void VisitProgram (Prog n)
        {
            n.Settings.Visit(this);
            n.Deal.Visit(this);
            n.Collateral.Visit(this);
            n.Securities.Visit(this);
            n.CreditPaymentRules.Visit(this);
            n.Simulation.Visit(this);
        }

        public override void VisitSettings(Settings n) { HandleInternalType("Settings", n); }
        public override void VisitDeal(Deal n) { HandleInternalType("Deal", n); }
        public override void VisitCollateral(Collateral n) { HandleInternalType("Collateral", n); }
        public override void VisitSecurities(Securities n) { HandleInternalType("Securities", n); }
        public override void VisitCreditPaymentRules(CreditPaymentRules n) { HandleInternalType("CreditPaymentRules", n); }
        public override void VisitSimulation(Simulation n) { HandleInternalType("Simulation", n); }

        private void HandleInternalType(string name, DeclarationClass n)
        {
            SetupInternalClass(n, name);

            n.Statements.Visit(this);
            _gen.Emit(OpCodes.Ret);
        }

        #endregion

        public override void VisitInstantiateClass(InstantiateClass n)
        {
            n.Actuals.Visit(this);
            var info = _typeManager.GetBuilderInfo(n.ClassName);
            _gen.Emit(OpCodes.Newobj, info.ConstructorBuilder.Builder);
            _lastWalkedType = info.Builder;
        }

        public override void VisitStatementList (StatementList n)
        {
            if (n.IsEmpty) return;

            n.Head.Visit(this);
            n.Tail.Visit(this);
        }

        public override void VisitStatementExpression (StatementExpression n)
        {
            n.Expression.Visit(this);
        }

        public override void VisitInvoke (Invoke n)
        {
            if (InternalMethodManager.IsSystemMethod(n.Method))
            {
                n.Actuals.Visit(this);
                var method = InternalMethodManager.Lookup(n.Method);
                method.Emit(_gen);
            }
            else
            {
                throw new TrancheCompilerException(string.Format("Unknown method {0} at {1}", n.Method, n.Location));
            }
        }

        public override void VisitAssign(Assign n)
        {
            _typeManager.AddField(_currentType, n); //we're treating everything like a field (for now)
            
            n.LValue.Visit(this);
            n.Expr.Visit(this);
            ApplyAssignmentCallback();
        }

        public override void VisitDereferenceField(DereferenceField n)
        {
            n.Object.Visit(this);
            var info = _typeManager.GetBuilderInfo(_lastWalkedType.Name);
            var field = info.FieldMap[n.Field];
            
            _lastWalkedType = field.FieldType;

            //either have to load the field if getting, or store it if setting
            // depends on who called me, assignment or dereference

            // UBER-HACK!!
            if (!n.IsLeftHandSide)
                _gen.Emit(Enum.GetNames(typeof(InternalTypes)).Contains(n.Field) ? OpCodes.Ldsfld : OpCodes.Ldfld, field);
            else
                _assignmentCallback = gen => gen.Emit(Enum.GetNames(typeof(InternalTypes)).Contains(n.Field) ? OpCodes.Stsfld : OpCodes.Stfld, field);
        }

        public override void VisitIdentifier(Identifier n)
        {
            var field = _currentTypeBuilder.FieldMap[n.Id];
            var shouldbeStatic = Enum.GetNames(typeof (InternalTypes)).Contains(field.Name);

            if(!shouldbeStatic)
            {
                _gen.Emit(OpCodes.Ldarg_0); //this.

                if(n.IsLeftHandSide)
                    _assignmentCallback = gen => gen.Emit(OpCodes.Stfld, field);
                else
                    _gen.Emit(OpCodes.Ldfld, field);
            }
            else
            {
                if (n.IsLeftHandSide)
                    _assignmentCallback = gen => gen.Emit(OpCodes.Stsfld, field);
                else
                    _gen.Emit(OpCodes.Ldsfld, field);
            }

            _lastWalkedType = field.FieldType;
        }

        public override void VisitStringLiteral (StringLiteral n)
        {
            var escaped = n.Value.Substring(1, n.Value.Length - 2);
            _gen.Emit(OpCodes.Ldstr, escaped);
            _lastWalkedType = typeof (string);
        }

        public override void VisitIntegerLiteral(IntegerLiteral n)
        {
            _gen.Emit(OpCodes.Ldc_I4, n.Value);
            _lastWalkedType = typeof(int);
        }

        public override void VisitRealLiteral(RealLiteral n)
        {
            _gen.Emit(OpCodes.Ldc_R4, n.Value);
            _lastWalkedType = typeof(double);
        }

        public override void VisitBooleanLiteral(BooleanLiteral n)
        {
            //true == 1 / false == 0
            _gen.Emit(n.Val ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
            _lastWalkedType = typeof(int);
        }


        private void SetupInternalClass(DeclarationClass n, string name)
        {
            _typeManager.AddClass(n);

            if (name.Equals(ENTRY_POINT, StringComparison.OrdinalIgnoreCase))
            {
                var methodInfo = CreateEntryPointMethod(n, name);
                _gen = methodInfo.Builder.GetILGenerator();

                foreach (var item in Enum.GetNames(typeof(InternalTypes)))
                {
                    var ident = new Identifier(n.Location, item);
                    var instantiate = new InstantiateClass(item, new ExpressionList());
                    var assign = new Assign(ident, instantiate) {InternalType = new TypeClass(item)};
                    
                    VisitAssign(assign);
                }

                _assemblyBuilder.SetEntryPoint(methodInfo.Builder);
            }
            else
            {
                var ctorInfo = CreateInternalClassCtor(n, name);
                _gen = ctorInfo.Builder.GetILGenerator();

                var baseCtor = typeof(object).GetConstructor(Type.EmptyTypes);
                _gen.Emit(OpCodes.Ldarg_0); //this.
                if (baseCtor != null) _gen.Emit(OpCodes.Call, baseCtor);
            }
        }

        //this is a special case since it will be the entry point..
        private MethodBuilderInfo CreateEntryPointMethod(DeclarationClass n, string name)
        {
            _currentType = name;
            _currentTypeBuilder = _typeManager.GetBuilderInfo(n.Name);

            var method = new DeclarationMethod(name, n.Statements)
                             {
                                 Type = new TypeFunction(true),
                                 Descriptor = new MethodDescriptor(n.Type, name, n.Descriptor)
                             };

            _typeManager.AddMethod(name, method);

            return _typeManager.GetMethodBuilderInfo(_currentType, n.Name);
        }

        private ConstructorBuilderInfo CreateInternalClassCtor(DeclarationClass n, string name)
        {
            _currentType = name;
            _currentTypeBuilder = _typeManager.GetBuilderInfo(n.Name);

            var method = new DeclarationMethod(name, n.Statements)
                             {
                                 Type = new TypeFunction(true),
                                 Descriptor = new MethodDescriptor(n.Type, name, n.Descriptor)
                             };

            _typeManager.AddCtor(name, method);
            return _currentTypeBuilder.ConstructorBuilder;
        }

        private void ApplyAssignmentCallback()
        {
            if (_assignmentCallback != null)
            {
                _assignmentCallback(_gen);
                _assignmentCallback = null;
            }
        }
    }
}
