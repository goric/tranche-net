using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SemanticAnalysis
{
    public class TrancheCompilerException : Exception
    {
        public TrancheCompilerException() : base() { }
        public TrancheCompilerException(string msg) : base(msg) { }
    }
}
