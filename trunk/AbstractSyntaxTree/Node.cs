using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using QUT.Gppg;

//using SemanticAnalysis;

namespace AbstractSyntaxTree
{
    public class Node
    {
        private const int TAB_SPACES = 4;

        public LexLocation Location { get; set; }
        //public virtual InternalType InternalType { get; set; }

        protected String NewLine (int depth)
        {
            var s = new StringBuilder(Environment.NewLine);

            for (int i = 0; i < depth * TAB_SPACES; i++)
                s.Append(' ');

            return s.ToString();
        }

        public virtual String Print (int depth)
        {
            return "";
        }

        public virtual void Visit (Visitor v)
        {
            v.VisitNode(this);
        }

        public string CheckNullPrint (Node n, int depth)
        {
            return (n != null) ? n.Print(depth) : string.Empty;
        }
    }
}
