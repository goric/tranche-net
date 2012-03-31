using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

using AbstractSyntaxTree;
using AbstractSyntaxTree.InternalTypes;
using SemanticAnalysis;
using TrancheLib;

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
        private AssemblyBuilder _assemblyBuilder;
        private ModuleBuilder _moduleBuilder;
        private ILGenerator _gen;
        private TypeManager _typeManager;
        private TypeBuilderInfo _currentTypeBuilder;
        private Type _lastWalkedType;

        private readonly string _assemblyName;
        private string _currentType;
        private Action<ILGenerator> _assignmentCallback;
        private int _internalListIndex;

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
        public override void VisitSimulation(Simulation n) { HandleInternalType("Simulation", n); }

        public override void VisitCollateral(Collateral n)
        {
            SetupInternalClass(n, "Collateral");

            AddListMemberToInternalClass("Collateral", "CollateralItems", "CollateralItem");

            n.Statements.Visit(this);
            _gen.Emit(OpCodes.Ret);
        }
        public override void VisitSecurities(Securities n) 
        {
            SetupInternalClass(n, "Securities");

            AddListMemberToInternalClass("Securities", "Bonds", "Bond");

            n.Statements.Visit(this);
            _gen.Emit(OpCodes.Ret);
        }
        public override void VisitCreditPaymentRules(CreditPaymentRules n) 
        {
            SetupInternalClass(n, "CreditPaymentRules");

            var bondType = _moduleBuilder.GetType("Bond");
            var ruleType = typeof (Rule<,,>).MakeGenericType(bondType, bondType, bondType);

            AddListMemberToInternalClass("CreditPaymentRules", "InterestRules", ruleType);
            AddListMemberToInternalClass("CreditPaymentRules", "PrincipalRules", ruleType);

            n.Statements.Visit(this);
            _gen.Emit(OpCodes.Ret);
        }
        public override void VisitPrincipalRules(PrincipalRules n)
        {
            var predType = typeof (Predicate<>).MakeGenericType(_moduleBuilder.GetType("Bond"));

            if (n.Statements == null) 
                return;
            /*
            n.Statements.Head.Visit(this);
            if(n.Statements.Joiner != null)
                n.Statements.Tail.Visit(this);
            */ 
        }

        private void HandleInternalType(string name, DeclarationClass n)
        {
            SetupInternalClass(n, name);

            if(n.Statements != null)
                n.Statements.Visit(this);

            _gen.Emit(OpCodes.Ret);
        }

        private void AddListMemberToInternalClass(string className, string fieldName, string underlyingClassName)
        {
            var type = _moduleBuilder.GetType(underlyingClassName);
            AddListMemberToInternalClass(className, fieldName, type);
        }

        private void AddListMemberToInternalClass(string className, string fieldName, Type underlyingType)
        {
            var listType = typeof(List<>).MakeGenericType(underlyingType);

            _internalListIndex = 0;
            _typeManager.AddInstanceField(className, fieldName, listType);
            var info = _typeManager.GetBuilderInfo(className);
            var field = info.FieldMap[fieldName];

            var ctor = TypeBuilder.GetConstructor(listType, typeof(List<>).GetConstructor(Type.EmptyTypes));
            _gen.Emit(OpCodes.Ldarg_0);
            _gen.Emit(OpCodes.Newobj, ctor);
            _gen.Emit(OpCodes.Stfld, field);
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

        public override void VisitCollateralItem(CollateralItem n)
        {
            var collatType = _moduleBuilder.GetType("CollateralItem");
            var ctor = _typeManager.GetBuilderInfo("CollateralItem").ConstructorBuilder.Builder;
            
            //new up a CollateralItem
            _gen.DeclareLocal(collatType);
            _gen.Emit(OpCodes.Newobj, ctor);
            _gen.Emit(OpCodes.Stloc, _internalListIndex);

            SetCurrentType("CollateralItem");
            if(n.Statements != null)
                n.Statements.Visit(this);
            SetCurrentType("Collateral");

            // add current item to the list member variable
            _gen.Emit(OpCodes.Ldarg_0);
            _gen.Emit(OpCodes.Ldfld, _typeManager.GetBuilderInfo(_currentType).FieldMap["CollateralItems"]);
            _gen.Emit(OpCodes.Ldloc, _internalListIndex);

            //get the generic add method and call it
            var listType = typeof(List<>).MakeGenericType(_moduleBuilder.GetType("CollateralItem"));
            var method = TypeBuilder.GetMethod(listType, typeof(List<>).GetMethod("Add"));
            _gen.Emit(OpCodes.Callvirt, method);

            _internalListIndex++;

            if(n.Tail != null && !n.Tail.IsEmpty)
                n.Tail.Visit(this);
        }

        public override void VisitBond(Bond n)
        {
            var collatType = _moduleBuilder.GetType("Bond");
            var ctor = _typeManager.GetBuilderInfo("Bond").ConstructorBuilder.Builder;

            _gen.DeclareLocal(collatType);
            _gen.Emit(OpCodes.Newobj, ctor);
            _gen.Emit(OpCodes.Stloc, _internalListIndex);

            SetCurrentType("Bond");
            if (n.Statements != null)
                n.Statements.Visit(this);
            SetCurrentType("Securities");

            // add current item to the list member variable
            _gen.Emit(OpCodes.Ldarg_0);
            _gen.Emit(OpCodes.Ldfld, _typeManager.GetBuilderInfo(_currentType).FieldMap["Bonds"]);
            _gen.Emit(OpCodes.Ldloc, _internalListIndex);

            //get the generic add method and call it
            var listType = typeof(List<>).MakeGenericType(_moduleBuilder.GetType("Bond"));
            var method = TypeBuilder.GetMethod(listType, typeof(List<>).GetMethod("Add"));
            _gen.Emit(OpCodes.Callvirt, method);

            _internalListIndex++;

            if (n.Tail != null && !n.Tail.IsEmpty)
                n.Tail.Visit(this);
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
            // depends on why we're dereferencing, to read or write
            var shouldbeStatic = Enum.GetNames(typeof(InternalTypes)).Contains(field.Name);
            if (!n.IsLeftHandSide)
                _gen.Emit(shouldbeStatic ? OpCodes.Ldsfld : OpCodes.Ldfld, field);
            else
                _assignmentCallback = gen => gen.Emit(shouldbeStatic ? OpCodes.Stsfld : OpCodes.Stfld, field);
        }

        public override void VisitIdentifier(Identifier n)
        {
            var field = _currentTypeBuilder.FieldMap[n.Id];
            var shouldbeStatic = Enum.GetNames(typeof (InternalTypes)).Contains(field.Name);

            if(!shouldbeStatic)
            {
                if(IsSubClass(_currentTypeBuilder))
                    _gen.Emit(OpCodes.Ldloc, _internalListIndex);
                else
                    _gen.Emit(OpCodes.Ldarg_0);

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

        public override void VisitEqual(Equal n)
        {
            n.Left.Visit(this);
            n.Right.Visit(this);
        }

        #region visit literals

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
            _gen.Emit(OpCodes.Ldc_R8, n.Value);
            _lastWalkedType = typeof(double);
        }

        public override void VisitBooleanLiteral(BooleanLiteral n)
        {
            //true == 1 / false == 0
            _gen.Emit(n.Val ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
            _lastWalkedType = typeof(int);
        }

        #endregion

        #region implementation details

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
            if (_assignmentCallback == null) return;

            _assignmentCallback(_gen);
            _assignmentCallback = null;
        }

        private void SetCurrentType(string name)
        {
            _currentType = name;
            _currentTypeBuilder = _typeManager.GetBuilderInfo(name);
        }

        private bool IsSubClass(TypeBuilderInfo b)
        {
            return new[] { "CollateralItem", "Bond", "InterestRules","PrincipalRules" }.Contains(b.Name);
        }

        #endregion
    }
}
