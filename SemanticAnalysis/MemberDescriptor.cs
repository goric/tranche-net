﻿/*  
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
