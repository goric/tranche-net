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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ILGen
{
    public static class InternalMethodManager
    {
        private static readonly Dictionary<string, InternalMethod> _methods = new Dictionary<string, InternalMethod>();
        public static IEnumerable<InternalMethod> Methods { get { return _methods.Values; } }

        static InternalMethodManager ()
        {
            var target = typeof(InternalMethod);

            Func<Type, bool> filter = p => p.IsSubclassOf(target) && !p.IsAbstract;
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(filter);

            foreach (var subType in types)
            {
                var instance = (InternalMethod)Activator.CreateInstance(subType);
                _methods.Add(instance.Name, instance);
            }
        }

        public static bool IsSystemMethod(string name)
        {
            return _methods.ContainsKey(name);
        }

        public static InternalMethod Lookup (string name)
        {
            return _methods[name];
        }
    }
}
