using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace ILGen.trancheMethods.Math
{
    class Ceil : InternalMethod
    {
        public Ceil () : base("ceil") { }

        public override void Emit (ILGenerator gen)
        {
            var methodInfo = typeof(System.Math).GetMethod("Ceil", PublicStatic, null, new Type[] { typeof(double) }, null);
            gen.Emit(OpCodes.Call, methodInfo);
        }
    }
}
