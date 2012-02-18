using System.Collections.Generic;

using System.Reflection;
using System.Reflection.Emit;
using SemanticAnalysis;

namespace ILGen
{
    public class ConstructorBuilderInfo : BuilderInfo
    {
        public ConstructorBuilderInfo(ConstructorBuilder builder)
            : this(builder, null) { }

        public ConstructorBuilderInfo(ConstructorBuilder builder, Dictionary<string, ArgumentInfo> formals)
            : base(builder, formals) { }

        public ConstructorBuilder Builder
        {
            get { return Method as ConstructorBuilder; }
        }
    }
}
