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
