using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AbstractSyntaxTree.InternalTypes;

namespace AbstractSyntaxTree
{
    public class Prog : Node
    {
        public Settings Settings { get; set; }
        public Deal Deal { get; set; }
        public Collateral Collateral { get; set; }
        public Securities Securities { get; set; }
        public CreditPaymentRules CreditPaymentRules { get; set; }
        public Simulation Simulation { get; set; }

        public Prog (Settings s, Deal d, Collateral c, Securities sec, CreditPaymentRules cpr, Simulation sim)
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
