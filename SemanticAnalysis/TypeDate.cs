using System;

namespace SemanticAnalysis
{
    public class TypeDate : InternalType
    {
        public override Type CilType { get { return typeof(DateTime); } }
        public override string ToString() { return "date"; }
    }
}
