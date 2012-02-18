using System.Collections.Generic;
using System.Reflection.Emit;
using SemanticAnalysis;

namespace ILGen.trancheMethods.Uncallable
{
    public class Simulation : InternalMethod
    {
        public Simulation ()
            : base("Simulation", new TypeVoid(), new Dictionary<string, InternalType> { })
        {

        }

        public override void Emit (ILGenerator gen)
        {

        }
    }
}
