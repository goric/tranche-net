using System;

namespace AbstractSyntaxTree
{
    public class Block : Statement
    {
        public Statement Body { get; set; }
        public bool IsBranch { get; set; }

        public Block (Statement body)
        {
            Body = body;
        }
        
        public override String Print(int depth)
        {
            return "{" + NewLine(depth + 1) + Body.Print(depth + 1) + NewLine(depth) + "}";
        }
    }
}
