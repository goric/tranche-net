using System;

namespace SemanticAnalysis
{
    public class TrancheCompilerException : Exception
    {
        public TrancheCompilerException() { }
        public TrancheCompilerException(string msg) : base(msg) { }
    }
}
