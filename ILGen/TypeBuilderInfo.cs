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
