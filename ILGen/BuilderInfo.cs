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

namespace ILGen
{
    public abstract class BuilderInfo
    {
        public virtual MethodBase Method { get; private set; }
        public Dictionary<string, LocalBuilderInfo> Locals { get; private set; }
        public Dictionary<string, ArgumentInfo> Arguments { get; private set; }

        protected BuilderInfo (MethodBase builderObj) : this(builderObj, null) { }
        protected BuilderInfo (MethodBase builderObj, Dictionary<string, ArgumentInfo> formals)
        {
            Method = builderObj;
            Locals = new Dictionary<string, LocalBuilderInfo>();
            Arguments = formals ?? new Dictionary<string, ArgumentInfo>();
        }

        public LocalBuilderInfo AddLocal (string name, LocalBuilder builder)
        {
            var info = new LocalBuilderInfo(Locals.Count, name, builder);
            Locals.Add(name, info);
            return info;
        }
    }
}
