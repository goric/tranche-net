using System;

namespace SemanticAnalysis
{
    public abstract class InternalType
    {
        public virtual bool IsSupertype (InternalType checkType)
        {
            /* This method should never return a value itself, but rather call the appropriate overload for the
             *  concrete type of checkType */
            dynamic realType = checkType;
            return this.IsSupertype(realType);
        }

        public virtual bool IsSupertype (TypeFunction checkType) { return false; }
        public virtual bool IsSupertype (TypeVoid checkType) { return false; }
        public virtual bool IsSupertype (TypeObject checkType) { return false; }
        public virtual bool IsSupertype (TypeClass checkType) { return false; }
        public virtual bool IsSupertype (TypeString checkType) { return false; }
        public virtual bool IsSupertype(TypeBoolean checkType) { return false; }
        public virtual bool IsSupertype(TypeReal checkType) { return false; }
        public virtual bool IsSupertype(TypeInteger checkType) { return false; }

        public virtual bool IsFunction { get { return false; } }
        public virtual bool IsClass { get { return false; } }
        public virtual bool IsString { get { return false; } }
        public virtual bool IsNumeric { get { return false; } }
        
        public virtual int Size { get { return 0; } }

        public abstract Type CilType { get; }

        public virtual string Print(int depth)
        {
            return string.Empty;
        }
    }
}
