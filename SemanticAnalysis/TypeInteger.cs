using System;

namespace SemanticAnalysis
{
    public class TypeInteger : InternalType
    {
        public override Type CilType { get { return typeof(int); } }
        public override bool IsNumeric { get { return true; } }
        public override string ToString() { return "integer"; }
        public override bool IsSupertype(TypeInteger checkType) { return true; }
        public override bool IsSupertype(TypeReal checkType) { return true; }
    }
}
