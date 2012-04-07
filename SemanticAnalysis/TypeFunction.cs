using System;
using System.Collections.Generic;

namespace SemanticAnalysis
{
    public class TypeFunction : InternalType
    {
        public override bool IsFunction { get { return true; } }

        public override Type CilType
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsConstructor { get; set; }
        public Scope Scope { get; set; }
        public InternalType ReturnType { get; set; }
        public string Name { get; private set; }

        public Dictionary<string, InternalType> Formals { get; set; }
        public Dictionary<string, InternalType> Locals { get; set; }

        public TypeFunction(string name)
            : this(false)
        {
            Name = name;
        }

        public TypeFunction(bool isCtor)
        {
            IsConstructor = isCtor;
            Formals = new Dictionary<string, InternalType>();
            Locals = new Dictionary<string, InternalType>();
        }

        public override string ToString() { return ""; }
    }
}
