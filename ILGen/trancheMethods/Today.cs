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
using System.Reflection;
using System.Reflection.Emit;

using SemanticAnalysis;

namespace ILGen.trancheMethods
{
    public class Today : InternalMethod
    {
        public Today () : base("today", new TypeVoid(), null) { }

        public override void Emit (ILGenerator gen)
        {
            gen.Emit(OpCodes.Call, typeof(DateTime).GetMethod("get_Today",
                BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(object) }, null));
        }
    }
}
