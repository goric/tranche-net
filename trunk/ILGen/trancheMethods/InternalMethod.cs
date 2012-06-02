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
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using SemanticAnalysis;

namespace ILGen
{
    public abstract class InternalMethod
    {
        public const BindingFlags PublicStatic = BindingFlags.Public | BindingFlags.Static;
        public const BindingFlags Public = BindingFlags.Public | BindingFlags.Instance;

        public string Name { get; private set; }
        public TypeFunction FuncInfo { get; set; }

        protected InternalMethod (string name)
        {
            Name = name;
        }
        protected InternalMethod (string name, InternalType returnType, Dictionary<string, InternalType> formals)
        {
            Name = name;
            FuncInfo = new TypeFunction(name)
            {
                ReturnType = returnType,
                Formals = formals
            };
        }


        public abstract void Emit (ILGenerator gen);
    }
}
