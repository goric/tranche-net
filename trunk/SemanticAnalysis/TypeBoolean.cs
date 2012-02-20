using System;

namespace SemanticAnalysis
{
    public class TypeBoolean : InternalType
    {
        public override Type CilType { get { return typeof(bool); } }
        public override string ToString() { return "boolean"; }
    }
}
