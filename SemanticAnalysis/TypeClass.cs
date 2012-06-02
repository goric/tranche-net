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
    public class TypeClass : InternalType
    {
        public override bool IsClass { get { return true; } }

        public string ClassName { get; set; }
        public ClassDescriptor Parent { get; set; }
        public ClassDescriptor Descriptor { get; set; }
        public Scope Scope { get; set; }

        public Dictionary<String, InternalType> Fields { get; set; }
        public Dictionary<String, InternalType> Methods { get; set; }

        public TypeClass (string name, ClassDescriptor parent = null)
        {
            ClassName = name;
            Parent = parent;

            Fields = new Dictionary<string, InternalType>();
            Methods = new Dictionary<string, InternalType>();
        }

        public void AddField (string name, InternalType type)
        {
            Fields.Add(name, type);
        }

        public void AddMethod (string name, InternalType type)
        {
            Methods.Add(name, type);
        }

        public override string ToString ()
        {
            return "class";
        }

        /// <summary>
        /// returns true if checkType is a super type of this
        /// </summary>
        /// <param name="checkType"></param>
        /// <returns></returns>
        public override bool IsSupertype (TypeClass checkType)
        {
            if (checkType.ClassName == ClassName)
                return true;
            if (Parent != null)
                return Parent.Type.IsSupertype(checkType);
            
            return false;
        }

        public override Type CilType
        {
            get { throw new NotImplementedException(); }
        }
    }
}
