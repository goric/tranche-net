using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ILGen
{
    public static class InternalMethodManager
    {
        private static readonly Dictionary<string, InternalMethod> _methods = new Dictionary<string, InternalMethod>();
        public static IEnumerable<InternalMethod> Methods { get { return _methods.Values; } }

        static InternalMethodManager ()
        {
            var target = typeof(InternalMethod);

            Func<Type, bool> filter = p => p.IsSubclassOf(target)
                                                && !p.IsAbstract;

            var types = Assembly.GetExecutingAssembly().GetTypes().Where(filter);

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

        public static InternalMethod Lookup (string name)
        {
            return _methods[name];
        }
    }
}
