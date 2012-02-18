
namespace AbstractSyntaxTree
{
    public class Declaration : Node
    {
        public override void Visit (Visitor v)
        {
            v.VisitDeclaration(this);
        }
    }
}
