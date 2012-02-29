using System;

using SemanticAnalysis;

namespace AbstractSyntaxTree
{
    public class DeclarationClass : Statement
    {
        public String Name { get; set; }
        public StatementList Statements { get; set; }
        public TypeClass Type { get; set; }
        public ClassDescriptor Descriptor { get; set; }

        public DeclarationClass(String name, StatementList decls)
        {
            Name = name;
            Statements = decls;
        }
        
        public override String Print(int depth)
        {
            return "class " + Name + "{" + NewLine(depth + 1) 
                + (Statements == null ? string.Empty : Statements.Print(depth + 1)) 
                + NewLine(depth) + "}";
        }

        public override void Visit (Visitor v)
        {
            v.VisitDeclarationClass(this);
        }
    }
}
