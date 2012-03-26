using System;
using System.Reflection;
using System.Reflection.Emit;

using SemanticAnalysis;

namespace ILGen.trancheMethods
{
    public class Today : InternalMethod
    {
        public Today () : base("today", new TypeVoid(), null) { }

        public override void Emit (ILGenerator gen)
        {
            gen.Emit(OpCodes.Call, typeof(DateTime).GetMethod("get_Today",
                BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(object) }, null));
        }
    }
}
