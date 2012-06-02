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
    public class TypeString : InternalType
    {
        /// <summary>
        /// Max string length - 65k?
        /// </summary>
        public override int Size
        {
            get
            {
                return Int32.MaxValue;
            }
        }

        public override bool IsSupertype(TypeString checkType)
        {
            return true;
        }

        public override bool IsString
        {
            get
            {
                return true;
            }
        }

        public override string ToString() { return "string"; }

        public override Type CilType
        {
            get { return typeof(string); }
        }
    }
}
