using System;

namespace SemanticAnalysis
{
    public class TypeReal : InternalType
    {
        public override Type CilType { get { return typeof (double); } }
        public override string ToString() { return "real"; }
    }
}
