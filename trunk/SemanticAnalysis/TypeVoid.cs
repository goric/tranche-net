using System;

namespace SemanticAnalysis
{
    public class TypeVoid : InternalType
    {
        public override int Size
        {
            get { return 0; }
        }

        public override bool IsSupertype (TypeVoid checkType)
        {
            return true;
        }

        public override string ToString () { return "void"; }

        public override Type CilType
        {
            get { return typeof(void); }
        }
    }
}
