
namespace AbstractSyntaxTree.InternalTypes
{
    public class Settings : Statement
    {
        public bool IsEmpty { get; set; }
        public StatementList Statements { get; set; }

        public Settings () { IsEmpty = true; }
        public Settings (StatementList stmt)
        {
            IsEmpty = false;
            Statements = stmt;
        }
        
        public override void Visit (Visitor v)
        {
            v.VisitSettings(this);
        }

        public override string Print (int depth)
        {
            return "Settings {" + NewLine(depth + 1) + (IsEmpty ? string.Empty : Statements.Print(depth + 1)) + NewLine(depth) + "}";
        }
    }
}
