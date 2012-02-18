using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SemanticAnalysis
{
    public class TypeString : InternalType
    {
        /// <summary>
        /// Max string length - 65k?
        /// </summary>
        public override int Size
        {
            get
            {
                return Int32.MaxValue;
            }
        }

        public override bool IsSupertype(TypeString checkType)
        {
            return true;
        }

        public override bool IsString
        {
            get
            {
                return true;
            }
        }

        public override string ToString() { return "string"; }

        public override Type CilType
        {
            get { return typeof(string); }
        }
    }
}
