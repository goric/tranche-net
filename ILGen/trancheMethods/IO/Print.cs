using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

using SemanticAnalysis;

namespace ILGen.trancheMethods.IO
{
    public class Print : InternalMethod
    {
        public Print ()
            : base("print", new TypeVoid(), new Dictionary<string, InternalType> { { "value", new TypeObject() } })
        {

        }

        public override void Emit (ILGenerator gen)
        {
            gen.Emit(OpCodes.Call, typeof(Console).GetMethod("Write",
                BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(object) }, null));
        }

    }
}
