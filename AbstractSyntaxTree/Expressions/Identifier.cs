using System;

using QUT.Gppg;

namespace AbstractSyntaxTree
{
    public class Identifier : Expression
    {
        public String Id { get; set; }

        public Identifier (LexLocation loc, String id)
        {
            Location = loc;
            Id = id;
        }

        public override String Print (int depth)
        {
            return Id;
        }

        public override void Visit (Visitor v)
        {
            v.VisitIdentifier(this);
        }
    }

}
