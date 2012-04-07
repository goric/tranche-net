using System;
using SemanticAnalysis;

namespace AbstractSyntaxTree
{
    public class DeclarationMethod : Node
    {
        public InternalType ReturnType { get; set; }
        public String Name { get; set; }
        public StatementList Body { get; set; }
        public InternalType Type { get; set; }
        public MethodDescriptor Descriptor { get; set; }

        public DeclarationMethod(String name, StatementList body)
        {
            ReturnType = new TypeVoid();
            Name = name;
            Body = body;
        }

        public override String Print(int depth)
        {
            return ReturnType.Print(depth) + " " + Name + "(" + ")" + "{" + NewLine(depth + 1)
                + (Body == null ? string.Empty : Body.Print(depth + 1)) + NewLine(depth) + "}";
        }

        public override void Visit (Visitor v)
        {
            v.VisitDeclarationMethod(this);
        }
    }
}
