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

namespace SemanticAnalysis
{
    /// <summary>
    /// Class for storing descriptors for actual parameters so we can check
    /// them against the formals for readonly status
    /// </summary>
    public class ActualDescriptor : Descriptor
    {
        public String Name { get; private set; }
        public string Modifier { get; private set; }
        public override bool IsType { get { return true; } }
        public bool IsFromFormal { get; set; }
        public bool IsFormalReadonly { get; set; }

        /// <summary>
        /// Takes a type and the index in the function of a parameter
        /// </summary>
        public ActualDescriptor (InternalType type, string modifier = null, string name = null)
            : base(type)
        {
            Type = type;
            Modifier = modifier;
            Name = name;
        }
    }
}
