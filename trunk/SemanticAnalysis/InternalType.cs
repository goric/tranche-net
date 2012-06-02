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
