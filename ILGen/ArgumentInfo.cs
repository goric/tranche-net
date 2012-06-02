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

namespace ILGen
{
    public class ArgumentInfo
    {
        public string Name { get; private set; }
        public Type CilType { get; private set; }
        public int Index { get; private set; }

        public ArgumentInfo (string name, Type cilType, int index)
        {
            Name = name;
            CilType = cilType;
            Index = index;
        }
    }
}
