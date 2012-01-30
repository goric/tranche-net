using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace ILGen.trancheMethods.IO
{
    public class FRead : InternalMethod
    {
        public FRead () : base("fread")
        {
        }

        public override void Emit (ILGenerator gen)
        {
            var methodInfo = typeof(File).GetMethod("ReadAllLines", PublicStatic, null, new Type[] { typeof(string) }, null);
            gen.Emit(OpCodes.Call, methodInfo);
        }
    }
}
