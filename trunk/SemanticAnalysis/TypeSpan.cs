using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SemanticAnalysis
{
    public class TypeSpan : InternalType
    {
        public override Type CilType { get { return typeof(TimeSpan); } }
        public override string ToString() { return "span"; }
    }
}
