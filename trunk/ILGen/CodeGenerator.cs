using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;

using AbstractSyntaxTree;
using SemanticAnalysis;

namespace ILGen
{
    public class CodeGenerator : Visitor
    {
        private readonly string _assemblyName;
        protected AssemblyBuilder _assemblyBuilder;
        protected ModuleBuilder _moduleBuilder;
        protected ILGenerator _gen;
        protected TypeManager _typeManager;
        protected BuilderInfo _currentMethodBuilder;

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

        public override void VisitSimulation (AbstractSyntaxTree.InternalTypes.Simulation n)
        {
            SetupInternalClass(n, "Simulation");
            
            n.Declarations.Visit(this);
            _gen.Emit(OpCodes.Ret);
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
        }

        public override void VisitStringLiteral (StringLiteral n)
        {
            var escaped = n.Value.Substring(1, n.Value.Length - 2);
            _gen.Emit(OpCodes.Ldstr, escaped);
        }


        private void SetupInternalClass(DeclarationClass n, string name)
        {
            _typeManager.AddClass(n);

            var method = new DeclarationMethod(new TypeVoid(), name, n.Declarations);
            method.Type = new TypeFunction(true) { Formals = new Dictionary<string, InternalType>() };
            method.Descriptor = new MethodDescriptor(n.Type, name, n.Descriptor);

            _typeManager.AddMethod(name, method);
            var methodInfo = _typeManager.GetMethodBuilderInfo(name, name);
            
            _gen = methodInfo.Builder.GetILGenerator();

            if (name == "Simulation") //entry point
            {
                _assemblyBuilder.SetEntryPoint(methodInfo.Builder);
            }
        }
    }
}
