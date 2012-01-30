using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ILGen.trancheMethods
{
    public static class InternalMethodManager
    {
        private static Dictionary<string, InternalMethod> _methods = new Dictionary<string, InternalMethod>();
        public static IEnumerable<InternalMethod> Methods { get { return _methods.Values; } }

        static InternalMethodManager ()
        {
            var target = typeof(InternalMethod);
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(p => p.IsSubclassOf(target) && !p.IsAbstract);

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
    }
}
