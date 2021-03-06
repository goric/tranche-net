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
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

using SemanticAnalysis;

namespace ILGen.trancheMethods.IO
{
    public class Print : InternalMethod
    {
        public Print ()
            : base("print", new TypeVoid(), new Dictionary<string, InternalType> { { "value", new TypeObject() } }) { }

        public override void Emit (ILGenerator gen)
        {
            gen.Emit(OpCodes.Call, typeof(Console).GetMethod("Write",
                BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(object) }, null));
        }
    }

    public class PrintLn : InternalMethod
    {
        public PrintLn()
            : base("println", new TypeVoid(), new Dictionary<string, InternalType> { { "value", new TypeObject() } }) { }

        public override void Emit(ILGenerator gen)
        {
            gen.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine",
                BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(object) }, null));
        }
    }
}
