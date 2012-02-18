using System.Collections.Generic;

namespace SemanticAnalysis
{
    public class Scope
    {
        public Scope Parent { get; protected set; }
        public string Name { get; protected set; }
        public Dictionary<string, Descriptor> Descriptors { get; protected set; }

        public Scope (string name, Scope parent)
        {
            Parent = parent;
            Name = name;
            Descriptors = new Dictionary<string, Descriptor>();
        }

        public bool HasSymbol (string identifier)
        {
            return Descriptors.ContainsKey(identifier);
        }
    }
}
