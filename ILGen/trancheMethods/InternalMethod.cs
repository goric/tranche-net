using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace ILGen.trancheMethods
{
    public abstract class InternalMethod
    {
        public const BindingFlags PublicStatic = BindingFlags.Public | BindingFlags.Static;
        public const BindingFlags Public = BindingFlags.Public | BindingFlags.Instance;

        public string Name { get; private set; }

        public InternalMethod (string name)
        {
            Name = name;
        }

        public abstract void Emit (ILGenerator gen);
    }
}
