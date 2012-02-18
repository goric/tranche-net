
namespace SemanticAnalysis
{
    public abstract class Descriptor
    {
        public InternalType Type { get; protected set; }
        public virtual bool IsType { get { return false; } }
        public virtual bool IsObject { get { return false; } }
        public virtual bool IsField { get { return false; } }
        public virtual bool IsMethod { get { return false; } }

        protected Descriptor (InternalType t) { Type = t; }
    }
}
