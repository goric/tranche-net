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
using System.Reflection.Emit;
using AbstractSyntaxTree;
using SemanticAnalysis;

namespace ILGen
{
    public class TypeManager
    {
        public Dictionary<string, TypeBuilderInfo> TypeBuilderMap { get; private set; }

        public ModuleBuilder Module { get; private set; }

        public TypeManager (ModuleBuilder module)
        {
            TypeBuilderMap = new Dictionary<string, TypeBuilderInfo>();
            Module = module;

            AddInternalClass("CollateralItem");
            AddInternalClass("Bond");
        }

        public void CreateAllTypes ()
        {
            foreach (var info in TypeBuilderMap.Values)
                info.Builder.CreateType();
        }

        public MethodBuilderInfo GetMethodBuilderInfo (string typeName, string methodName)
        {
            return TypeBuilderMap[typeName].MethodMap[methodName];
        }

        public void AddClass(DeclarationClass n)
        {
            TypeBuilderMap.Add(n.Name, new TypeBuilderInfo(n, Module));
        }

        public void AddField(string typeName, Assign n)
        {
            var info = TypeBuilderMap[typeName];
            if (info.FieldMap.ContainsKey(n.Name)) return;

            var flags = FieldAttributes.Public;
            if (Enum.GetNames(typeof(InternalTrancheTypes)).Contains(n.Name) || typeName == "Simulation")
                flags |= FieldAttributes.Static;

            var type = n.Qualifier == null ? LookupCilType(n.InternalType) : LookupCilType(n.Qualifier.InternalType);
            var fieldBuilder = info.Builder.DefineField(n.Name, type, flags);
            info.FieldMap.Add(n.Name, fieldBuilder);
        }
        public void AddInstanceField(string typeName, string fieldName, Type type)
        {
            var info = TypeBuilderMap[typeName];
            var fieldBuilder = info.Builder.DefineField(fieldName, type, FieldAttributes.Public);
            info.FieldMap.Add(fieldName, fieldBuilder);
        }

        public void AddCtor(string typeName, DeclarationMethod n)
        {
            var info = TypeBuilderMap[typeName];
            var function = n.Type as TypeFunction;

            //simulation is the entry point, must be static
            var attributes = n.Name.Equals("Simulation", StringComparison.OrdinalIgnoreCase)
                                ? MethodAttributes.Public | MethodAttributes.Static
                                : MethodAttributes.Public;

            var builderObj = info.Builder.DefineConstructor(attributes, CallingConventions.Standard, ArgumentTypes(function));
            var formals = n.Descriptor == null ? new List<FormalDescriptor>() : n.Descriptor.Formals;
            info.ConstructorBuilder = new ConstructorBuilderInfo(builderObj, BuildFormalMap(formals));
        }

        public void AddMethod (string typeName, DeclarationMethod n)
        {
            var info = TypeBuilderMap[typeName];
            //simulation is the entry point, must be static
            var attributes = n.Name.Equals("Simulation", StringComparison.OrdinalIgnoreCase)
                                ? MethodAttributes.Public | MethodAttributes.Static
                                : MethodAttributes.Public;

            if(InternalMethodManager.IsSystemMethod(n.Name))
            {
                var method = InternalMethodManager.Lookup(n.Name);
                var funcInfo = method.FuncInfo;
                var formals = funcInfo.Formals.Values.Select(LookupCilType);
                var m = info.Builder.DefineMethod(n.Name,
                                                  attributes,
                                                  LookupCilType(funcInfo.ReturnType),
                                                  formals.ToArray());

                //store this MethodBuilder, keyed off its name
                info.MethodMap.Add(n.Name, new MethodBuilderInfo(m, BuildFormalMap(n.Descriptor.Formals)));
                return;
            }


            //we need to know the CIL type for the return type and arguments
            var returnType = LookupCilType(n.ReturnType);
            var function = (TypeFunction) n.Type;

            var methodBuilder = info.Builder.DefineMethod(n.Name,
                                                          attributes,
                                                          returnType,
                                                          function.Formals.Values.Select(LookupCilType).ToArray());

            //store this MethodBuilder, keyed off its name
            info.MethodMap.Add(n.Name, new MethodBuilderInfo(methodBuilder, BuildFormalMap(n.Descriptor.Formals)));
        }

        public TypeBuilderInfo GetBuilderInfo(string typeName)
        {
            return TypeBuilderMap[typeName];
        }

        private Dictionary<string, ArgumentInfo> BuildFormalMap (IEnumerable<FormalDescriptor> formals)
        {
            var map = new Dictionary<string, ArgumentInfo>();
            foreach (var f in formals)
            {
                var info = new ArgumentInfo(f.Name, LookupCilType(f.Type), map.Count);
                map.Add(f.Name, info);
            }
            return map;
        }

        public Type LookupCilType (InternalType type)
        {
            if (type is TypeClass)
            {
                var name = (type as TypeClass).ClassName;
                return TypeBuilderMap[name].Builder;
            }
            
            return type.CilType;
        }

        private Type[] ArgumentTypes(TypeFunction f)
        {
            return f.Formals.Values.Select(LookupCilType).ToArray();
        }

        private void AddInternalClass(string name)
        {
            var decl = new DeclarationClass(name, new StatementList());
            TypeBuilderMap.Add(name, new TypeBuilderInfo(decl, Module));
            var entry = TypeBuilderMap[name];
            var builderObj = entry.Builder.DefineDefaultConstructor(MethodAttributes.Public);            
            entry.ConstructorBuilder = new ConstructorBuilderInfo(builderObj);
        }
    }
}
