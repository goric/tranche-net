using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using SemanticAnalysis;

namespace ILGen
{
    public abstract class InternalMethod
    {
        public const BindingFlags PublicStatic = BindingFlags.Public | BindingFlags.Static;
        public const BindingFlags Public = BindingFlags.Public | BindingFlags.Instance;

        public string Name { get; private set; }
        public TypeFunction FuncInfo { get; set; }

        protected InternalMethod (string name)
        {
            Name = name;
        }
        protected InternalMethod (string name, InternalType returnType, Dictionary<string, InternalType> formals)
        {
            Name = name;
            FuncInfo = new TypeFunction(name)
            {
                ReturnType = returnType,
                Formals = formals
            };
        }


        public abstract void Emit (ILGenerator gen);
    }
}
