using System;

namespace AbstractSyntaxTree
{
    public class Expression : Node
    {
        public override String Print (int depth)
        {
            return "";
        }

        public override void Visit (Visitor v)
        {
        }
    }
}
