using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractSyntaxTree
{
    public class Statement : Node
    {
        public Statement ()
        {
        }

        public override void Visit (Visitor v)
        {
            v.VisitStatement(this);
        }

        /// <summary>
        /// Takes a statement, and if it's not already a block, wraps it in an Block.
        /// </summary>
        /// <returns></returns>
        public Block WrapInBlock()
        {
            if (this is Block)
                return (Block)this;
            else
                return new Block(this);
        }
    }
}
