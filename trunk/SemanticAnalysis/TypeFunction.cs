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
