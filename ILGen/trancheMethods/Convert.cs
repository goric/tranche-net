using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using SemanticAnalysis;

namespace ILGen.trancheMethods
{
    public class ToString : InternalMethod
    {
        public ToString() : base("toString", new TypeVoid(), new Dictionary<string, InternalType> { { "value", new TypeObject() } }) { }

        public override void Emit (ILGenerator gen)
        {
            gen.Emit(OpCodes.Call, typeof(Object).GetMethod("ToString",
                BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(object) }, null));
        }
    }

    public class IntParse : InternalMethod
    {
        public IntParse() : base("intParse", new TypeInteger(), new Dictionary<string, InternalType> { { "value", new TypeObject() } }) { }

        public override void Emit (ILGenerator gen)
        {
            gen.Emit(OpCodes.Call, typeof(int).GetMethod("Parse",
                BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(object) }, null));
        }
    }

    public class RealParse : InternalMethod
    {
        public RealParse() : base("realParse", new TypeReal(), new Dictionary<string, InternalType> { { "value", new TypeObject() } }) { }

        public override void Emit(ILGenerator gen)
        {
            gen.Emit(OpCodes.Call, typeof(double).GetMethod("Parse",
                BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(object) }, null));
        }
    }

    public class BooleanParse : InternalMethod
    {
        public BooleanParse() : base("BooleanParse", new TypeBoolean(), new Dictionary<string, InternalType> { { "value", new TypeObject() } }) { }

        public override void Emit(ILGenerator gen)
        {
            gen.Emit(OpCodes.Call, typeof(bool).GetMethod("Parse",
                BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(object) }, null));
        }
    }
}
