/*  
 * tranche.NET - a DSL for modeling structured finance products.
 * Copyright (C) 2012 Timothy Goric
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
*/
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

        public ClassDescriptor AddClass (string name, InternalType t)
        {
            var cd = new ClassDescriptor(t, name, CurrentScope);
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

        private static Descriptor Find(string name, Func<Descriptor, bool> pred, Scope s, bool currentOnly = false)
        {
            var checkScope = s;

            while (checkScope != null)
            {
                if (checkScope.HasSymbol(name))
                {
                    var d = checkScope.Descriptors[name];
                    if (pred(d))
                        return d;
                }
                checkScope = !currentOnly ? checkScope.Parent : null;
            }

            return null;
        }

        public Descriptor GetType(string name)
        {
            return Find(name, d => d.IsType);
        }
        public bool HasSymbol(string identifier)
        {
            return HasSymbol(identifier, CurrentScope);
        }
        public bool HasSymbol(string identifier, Scope s)
        {
            return Find(identifier, d => true, s) != null;
        }
        public bool HasSymbolShallow(string identifier)
        {
            return Find(identifier, d => true, CurrentScope, true) != null;
        }
    }
}
