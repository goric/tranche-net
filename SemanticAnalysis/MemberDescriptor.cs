using System.Collections.Generic;

namespace SemanticAnalysis
{
    public class MemberDescriptor : Descriptor
    {
        public override bool IsType { get { return true; } }
        public List<string> Modifiers { get; private set; }
        public string Name { get; private set; }
        public ClassDescriptor ContainingClass { get; set; }

        public MemberDescriptor (InternalType type, string name, ClassDescriptor cls) : base(type)
        {
            Modifiers = new List<string>();
            Name = name;
            ContainingClass = cls;
        }
    }
}
