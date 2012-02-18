using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace ILGen
{
    public class MethodBuilderInfo : BuilderInfo
    {
        public MethodBuilder Builder { get { return Method as MethodBuilder; } }

        public MethodBuilderInfo (MethodBuilder builder) : this(builder, null) { }
        public MethodBuilderInfo (MethodBase builder, Dictionary<string, ArgumentInfo> formals) : base(builder, formals)
        {
        }
    }
}
