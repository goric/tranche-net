
namespace AbstractSyntaxTree.InternalTypes
{
    public class Collateral : Statement
    {
        public StatementList CollateralItems { get; set; }

        public Collateral (StatementList items)
        {
            CollateralItems = items;
        }
        
        public override void Visit (Visitor v)
        {
            v.VisitCollateral(this);
        }

        public override string Print (int depth)
        {
            return "Collateral {" + NewLine(depth + 1) + CollateralItems.Print(depth + 1) + NewLine(depth) + "}";
        }
    }
}
