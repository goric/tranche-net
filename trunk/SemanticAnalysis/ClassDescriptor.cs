using System.Collections.Generic;

namespace SemanticAnalysis
{
    public class ClassDescriptor : Descriptor
    {
        public override bool IsType { get { return true; } }
        
        public List<MethodDescriptor> Methods { get; private set; }
        public List<MemberDescriptor> Fields { get; private set; }
        public Scope Scope { get; set; }
        public string Name { get; set; }

        public ClassDescriptor (InternalType t, string name, Scope s) : base(t)
        {
            Methods = new List<MethodDescriptor>();
            Fields = new List<MemberDescriptor>();
            Name = name;
            Scope = s;
        }

        public override string ToString ()
        {
            return "InternalType_Class<" + Type + ">";
        }
    }
}
