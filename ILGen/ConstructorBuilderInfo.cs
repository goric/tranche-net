using System.Collections.Generic;

using System.Reflection;
using System.Reflection.Emit;

namespace ILGen
{
    public class ConstructorBuilderInfo : BuilderInfo
    {
        public ConstructorBuilderInfo(MethodBase builder)
            : this(builder, null) { }

        public ConstructorBuilderInfo(MethodBase builder, Dictionary<string, ArgumentInfo> formals)
            : base(builder, formals) { }

        public ConstructorBuilder Builder
        {
            get { return Method as ConstructorBuilder; }
        }
    }
}
