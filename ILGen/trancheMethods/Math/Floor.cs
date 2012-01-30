using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace ILGen.trancheMethods.Math
{
    class Floor : InternalMethod
    {
        public Floor () : base("floor") { }

        public override void Emit (ILGenerator gen)
        {
            var methodInfo = typeof(System.Math).GetMethod("Floor", PublicStatic, null, new Type[] { typeof(double) }, null);
            gen.Emit(OpCodes.Call, methodInfo);
        }
    }
}
