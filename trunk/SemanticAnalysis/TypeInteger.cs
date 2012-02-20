using System;

namespace SemanticAnalysis
{
    public class TypeInteger : InternalType
    {
        public override Type CilType { get { return typeof(int); } }
        public override string ToString() { return "integer"; }
    }
}
