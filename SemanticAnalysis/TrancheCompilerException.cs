using System;

namespace SemanticAnalysis
{
    public class TrancheCompilerException : Exception
    {
        public TrancheCompilerException(string msg) : base(msg) { }
    }
}
