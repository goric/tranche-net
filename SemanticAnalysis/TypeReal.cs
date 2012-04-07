using System;

namespace SemanticAnalysis
{
    public class TypeReal : InternalType
    {
        public override Type CilType { get { return typeof (double); } }
        public override bool IsNumeric { get { return true; } }
        public override string ToString() { return "real"; }
        public override bool IsSupertype(TypeReal checkType) { return true; }
    }
}
