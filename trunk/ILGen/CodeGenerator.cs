using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;

using AbstractSyntaxTree;
using AbstractSyntaxTree.InternalTypes;
using SemanticAnalysis;

namespace ILGen
{
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

        public override void VisitProgram (Prog n)
        {
            n.Settings.Visit(this);
            n.Deal.Visit(this);
            n.Collateral.Visit(this);
            n.Securities.Visit(this);
            n.CreditPaymentRules.Visit(this);
            n.Simulation.Visit(this);
        }

        #region Internal Types

        public override void VisitSettings(Settings n)
        {
            SetupInternalClass(n, "Settings");

            n.Statements.Visit(this);
            _gen.Emit(OpCodes.Ret);
        }

        public override void VisitSimulation (Simulation n)
        {
            SetupInternalClass(n, "Simulation");
            
            n.Statements.Visit(this);
            _gen.Emit(OpCodes.Ret);
        }

        #endregion

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

        /*public override void VisitStatementVariable(StatementVariable n)
        {
            _typeManager.AddField(_currentType, n);
            n.Expression.Visit(this);

            FieldBuilder field = _currentTypeBuilder.FieldMap[n.Name];
            //push the "this" argument
            _gen.Emit(OpCodes.Ldarg_0);
            _gen.Emit(OpCodes.Stfld, field);
        }*/

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

        //we treat all variables as members...
        public override void VisitAssign(Assign n)
        {
            _typeManager.AddField(_currentType, n);

            n.LValue.Visit(this);
            n.Expr.Visit(this);
            ApplyAssignmentCallback();
        }

        public override void VisitIdentifier(Identifier n)
        {
            FieldBuilder field = _currentTypeBuilder.FieldMap[n.Id];
            //push the "this" argument
            _gen.Emit(OpCodes.Ldarg_0);
            _assignmentCallback = gen => gen.Emit(OpCodes.Stfld, field);
        }

        public override void VisitStringLiteral (StringLiteral n)
        {
            var escaped = n.Value.Substring(1, n.Value.Length - 2);
            _gen.Emit(OpCodes.Ldstr, escaped);
        }


        private void SetupInternalClass(DeclarationClass n, string name)
        {
            _typeManager.AddClass(n);

            var methodInfo = CreateInternalClassCtor(n, name);
            _gen = methodInfo.Builder.GetILGenerator();

            if (name.Equals(ENTRY_POINT, StringComparison.OrdinalIgnoreCase))
            {
                _assemblyBuilder.SetEntryPoint(methodInfo.Builder);
            }
        }

        private MethodBuilderInfo CreateInternalClassCtor(DeclarationClass n, string name)
        {
            _currentType = name;
            _currentTypeBuilder = _typeManager.GetBuilderInfo(n.Name);

            var method = new DeclarationMethod(new TypeVoid(), name, n.Statements);
            method.Type = new TypeFunction(true) { Formals = new Dictionary<string, InternalType>() };
            method.Descriptor = new MethodDescriptor(n.Type, name, n.Descriptor);

            _typeManager.AddMethod(name, method);
            return _typeManager.GetMethodBuilderInfo(name, name);
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
