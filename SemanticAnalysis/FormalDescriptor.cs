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

namespace SemanticAnalysis
{
    /// <summary>
    /// Class for storing descriptors for formal parameters so actual parameters
    /// can be type checked against them
    /// </summary>
    public class FormalDescriptor : Descriptor
    {
        public string Name { get; private set; }

        public string Modifier { get; set; }

        public override bool IsType { get { return true; } }

        /// <summary>
        /// Takes a type and the index in the function of a parameter
        /// </summary>
        public FormalDescriptor (InternalType type, string name, string modifier) : base(type)
        {
            Name = name;
            Modifier = modifier;
        }
    }
}
