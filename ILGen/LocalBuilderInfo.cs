using System.Reflection.Emit;

namespace ILGen
{
    public class LocalBuilderInfo
    {
        public int Index { get; private set; }
        public string Name { get; private set; }
        public LocalBuilder Builder { get; private set; }

        public LocalBuilderInfo (int index, string name, LocalBuilder builder)
        {
            Index = index;
            Name = name;
            Builder = builder;
        }
    }
}
