using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using QUT.Gppg;

namespace AbstractSyntaxTree
{
    public class Identifier : Expression
    {
        public String ID { get; set; }

        public Identifier (LexLocation loc, String id)
        {
            Location = loc;
            ID = id;
        }

        public override String Print (int depth)
        {
            return ID;
        }

        public override void Visit (Visitor v)
        {
            v.VisitIdentifier(this);
        }
    }

}
