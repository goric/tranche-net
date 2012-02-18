using System.Collections.Generic;
using System.Reflection.Emit;
using SemanticAnalysis;

namespace ILGen.trancheMethods.Uncallable
{
    public class Deal : InternalMethod
    {
        public Deal ()
            : base("Deal", new TypeVoid(), new Dictionary<string, InternalType> { })
        {

        }

        public override void Emit (ILGenerator gen)
        {

        }
    }
}
