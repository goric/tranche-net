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

using AbstractSyntaxTree;

namespace ILGen
{
    public class TypeBuilderInfo
    {
        public TypeBuilder Builder { get; private set; }
        public string Name { get { return Builder.Name; } }
        public Dictionary<string, MethodBuilderInfo> MethodMap { get; private set; }
        public Dictionary<string, FieldBuilder> FieldMap { get; private set; }
        public ConstructorBuilderInfo ConstructorBuilder { get; set; }

        public TypeBuilderInfo (DeclarationClass n, ModuleBuilder module)
        {
            Builder = module.DefineType(n.Name, TypeAttributes.Public);
            Init();
        }

        private void Init ()
        {
            MethodMap = new Dictionary<string, MethodBuilderInfo>();
            FieldMap = new Dictionary<string, FieldBuilder>();
        }
    }
}
