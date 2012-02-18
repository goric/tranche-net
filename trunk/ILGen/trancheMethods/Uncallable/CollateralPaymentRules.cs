using System.Collections.Generic;
using System.Reflection.Emit;
using SemanticAnalysis;

namespace ILGen.trancheMethods.Uncallable
{
    public class CollateralPaymentRules : InternalMethod
    {
        public CollateralPaymentRules ()
            : base("CollateralPaymentRules", new TypeVoid(), new Dictionary<string, InternalType> { })
        {

        }

        public override void Emit (ILGenerator gen)
        {

        }
    }
}
