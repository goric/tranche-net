
namespace AbstractSyntaxTree
{
    public class Prog : Node
    {
        public DeclarationClass Settings { get; set; }
        public DeclarationClass Deal { get; set; }
        public DeclarationClass Collateral { get; set; }
        public DeclarationClass Securities { get; set; }
        public DeclarationClass CreditPaymentRules { get; set; }
        public DeclarationClass Simulation { get; set; }

        public Prog(DeclarationClass s, DeclarationClass d, DeclarationClass c, DeclarationClass sec, DeclarationClass cpr, DeclarationClass sim)
        {
            Settings = s;
            Deal = d;
            Collateral = c;
            Securities = sec;
            CreditPaymentRules = cpr;
            Simulation = sim;
        }

        public override void Visit (Visitor v)
        {
            v.VisitProgram(this);
        }

        public override string Print (int depth)
        {
            return "Program: "
                + NewLine(depth + 1)
                + Settings.Print(depth + 1)
                + NewLine(depth + 1)
                + Deal.Print(depth + 1)
                + NewLine(depth + 1)
                + Collateral.Print(depth + 1)
                + NewLine(depth + 1)
                + Securities.Print(depth + 1)
                + NewLine(depth + 1)
                + CreditPaymentRules.Print(depth + 1)
                + NewLine(depth + 1)
                + Simulation.Print(depth + 1)
                + NewLine(depth + 1);
        }
    }
}
