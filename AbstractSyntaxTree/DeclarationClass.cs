using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SemanticAnalysis;

namespace AbstractSyntaxTree
{
    public class DeclarationClass : Statement
    {
        public String Name { get; set; }
        public StatementList Declarations { get; set; }
        public TypeClass Type { get; set; }
        public ClassDescriptor Descriptor { get; set; }

        public DeclarationClass(String name, StatementList decls)
        {
            Name = name;
            Declarations = decls;
        }
        
        public override String Print(int depth)
        {
            return "class " + Name + "{" + NewLine(depth + 1) 
                + (Declarations == null ? string.Empty : Declarations.Print(depth + 1)) 
                + NewLine(depth) + "}";
        }

        public override void Visit (Visitor v)
        {
            v.VisitDeclarationClass(this);
        }
    }
}
