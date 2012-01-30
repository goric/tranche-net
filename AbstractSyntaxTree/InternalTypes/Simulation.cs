using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractSyntaxTree.InternalTypes
{
    public class Simulation : Statement
    {
        public StatementList Statements { get; set; }

        public Simulation (StatementList items)
        {
            Statements = items;
        }
        
        public override void Visit (Visitor v)
        {
            v.VisitSimulation(this);
        }

        public override string Print (int depth)
        {
            return "Simulation {" + NewLine(depth + 1) + Statements.Print(depth + 1) + NewLine(depth) + "}";
        }
    }
}
