
namespace AbstractSyntaxTree.InternalTypes
{
    public class Deal : Statement
    {
        public bool IsEmpty { get; set; }
        public StatementList Statements { get; set; }

        public Deal () { IsEmpty = true; }
        public Deal (StatementList stmt)
        {
            IsEmpty = false;
            Statements = stmt;
        }
        
        public override void Visit (Visitor v)
        {
            v.VisitDeal(this);
        }

        public override string Print (int depth)
        {
            return "Deal {" + NewLine(depth + 1) + (IsEmpty ? string.Empty : Statements.Print(depth + 1)) + NewLine(depth) + "}";
        }
    }
}
