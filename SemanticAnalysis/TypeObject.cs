using System;

namespace SemanticAnalysis
{
    public class TypeObject : InternalType
    {
        public override bool IsSupertype (TypeFunction checkType) { return true; }
        public override bool IsSupertype (TypeVoid checkType) { return true; }

        public override Type CilType
        {
            get { return typeof(object); }
        }
    }
}
