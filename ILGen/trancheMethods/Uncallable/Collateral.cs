using System.Collections.Generic;
using System.Reflection.Emit;
using SemanticAnalysis;

namespace ILGen.trancheMethods.Uncallable
{
    public class Collateral : InternalMethod
    {
        public Collateral ()
            : base("Collateral", new TypeVoid(), new Dictionary<string, InternalType> { })
        {

        }

        public override void Emit (ILGenerator gen)
        {
            
        }
    }
}
