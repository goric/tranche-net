using System;
using System.Reflection.Emit;

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
        public virtual bool IsSupertype(TypeString checkType) { return false; }

        public virtual bool IsFunction { get { return false; } }
        public virtual bool IsClass { get { return false; } }
        public virtual bool IsString { get { return false; } }
        
        public virtual int Size { get { return 0; } }

        public abstract Type CilType { get; }

        public virtual InternalType BaseType { get { return this; } set { /* do nothing */} }

        public bool IsSubtypeOf (InternalType t)
        {
            return t.IsSupertype(this);
        }

        public virtual OpCode LoadElementOpCode
        {
            get { return OpCodes.Ldelem_Ref; }
        }

        public virtual OpCode StoreElementOpCode
        {
            get { return OpCodes.Stelem_Ref; }
        }

        public virtual string Print(int depth)
        {
            return string.Empty;
        }
    }
}
