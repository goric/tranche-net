using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

using SemanticAnalysis;

namespace ILGen
{
    public class UncallableInternalMethod : InternalMethod
    {
        public UncallableInternalMethod(string name) : base(name) { }
        public UncallableInternalMethod(string name, InternalType returnType, Dictionary<string, InternalType> formals)
            : base(name, returnType, formals)
        { 
        }

        public override void Emit(ILGenerator gen) { }
    }
}
