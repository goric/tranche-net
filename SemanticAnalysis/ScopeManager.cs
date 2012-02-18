using System;
using System.Collections.Generic;

namespace SemanticAnalysis
{
    public class ScopeManager
    {
        public Scope CurrentScope { get; set; }
        public Scope TopScope { get; private set; }

        public ScopeManager ()
        {
            CurrentScope = new Scope("top", null);
            TopScope = CurrentScope;
        }

        public Scope PushScope (string name)
        {
            CurrentScope = new Scope(name, CurrentScope);
            return CurrentScope;
        }
        public Scope PopScope ()
        {
            var old = CurrentScope;
            CurrentScope = CurrentScope.Parent;
            return old;
        }

        public void RestoreScope (Scope s)
        {
            CurrentScope = s;
        }

        public ClassDescriptor AddClass (string name, InternalType t, ClassDescriptor parent = null)
        {
            var cd = new ClassDescriptor(t, parent, name, CurrentScope);
            CurrentScope.Descriptors.Add(name, cd);
            return cd;
        }

        public MethodDescriptor AddMethod (string name, InternalType type, TypeClass containingClass, List<String> modifiers = null, bool isInternal = false)
        {
            var md = new MethodDescriptor(type, name, containingClass.Descriptor) {IsInternalMethod = isInternal};

            if (modifiers != null)
                md.Modifiers.AddRange(modifiers);
            
            CurrentScope.Descriptors.Add(name, md);
            containingClass.Descriptor.Methods.Add(md);
            return md;
        }

        public MemberDescriptor AddMember(string name, InternalType type, TypeClass containingClass)
        {
            var descriptor = new MemberDescriptor(type, name, containingClass.Descriptor);
            CurrentScope.Descriptors.Add(name, descriptor);
            containingClass.Descriptor.Fields.Add(descriptor);
            return descriptor;
        }


        public Descriptor Find(string name, Func<Descriptor, bool> pred)
        {
            return Find(name, pred, CurrentScope);
        }
        public Descriptor Find(string name, Func<Descriptor, bool> pred, Scope s, bool currentOnly = false)
        {
            Scope checkScope = s;

            while (checkScope != null)
            {
                if (checkScope.HasSymbol(name))
                {
                    Descriptor d = checkScope.Descriptors[name];
                    if (pred(d))
                        return d;
                }
                if (!currentOnly)
                    checkScope = checkScope.Parent;
                else
                    checkScope = null;
            }

            return null;
        }
    }
}
