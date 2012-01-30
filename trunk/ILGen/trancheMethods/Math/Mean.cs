using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace ILGen.trancheMethods.Math
{
    class Mean : InternalMethod
    {
        public Mean () : base("mean") { }

        public override void Emit (ILGenerator gen)
        {
            throw new NotImplementedException();
        }
    }
}
