using System;

namespace ILGen
{
    public class ArgumentInfo
    {
        public string Name { get; private set; }
        public Type CilType { get; private set; }
        public int Index { get; private set; }

        public ArgumentInfo (string name, Type cilType, int index)
        {
            Name = name;
            CilType = cilType;
            Index = index;
        }
    }
}
