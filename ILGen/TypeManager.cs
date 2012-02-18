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
            TypeBuilderInfo info = TypeBuilderMap[typeName];
            FieldBuilder fieldBuilder = info.Builder.DefineField(n.Name, LookupCilType(n.InternalType), FieldAttributes.Public);
            info.FieldMap.Add(n.Name, fieldBuilder);
        }

        public void AddCtor(string typeName, DeclarationMethod n)
        {
            TypeBuilderInfo info = TypeBuilderMap[typeName];
            TypeFunction function = n.Type as TypeFunction;

            //simulation is the entry point, must be static
            var attributes = n.Name.Equals("Simulation", StringComparison.OrdinalIgnoreCase)
                                ? MethodAttributes.Public | MethodAttributes.Static
                                : MethodAttributes.Public;

            ConstructorBuilder builderObj = info.Builder.DefineConstructor(attributes, CallingConventions.Standard, ArgumentTypes(function));

            info.ConstructorBuilder = new ConstructorBuilderInfo(builderObj, BuildFormalMap(n.Descriptor.Formals));
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
            return f.Formals.Values.Select(c => LookupCilType(c)).ToArray();
        }
    }
}
