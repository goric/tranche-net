
using SemanticAnalysis;

namespace AbstractSyntaxTree.InternalTypes
{
    public class Simulation : DeclarationClass
    {
        public Simulation (StatementList stmts) : base("Simulation", stmts) { }
        
        public override void Visit (Visitor v)
        {
            v.VisitSimulation(this);
        }
    }
}
