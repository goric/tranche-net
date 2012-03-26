namespace AbstractSyntaxTree
{
    public class Loop : Statement
    {
        public string LoopVariable { get; set; }
        public Expression InitialValue { get; set; }
        public Expression EndingValue { get; set; }
        public string StepDirection { get; set; }
        public Expression Step { get; set; }
        public StatementList Statements { get; set; }

        public Loop(string loopVariable, Expression from, string upDown, Expression to, StatementList stmts)
        {
            LoopVariable = loopVariable;
            InitialValue = from;
            EndingValue = to;
            StepDirection = upDown;
            Step = null;
            Statements = stmts;
        }
        public Loop(string loopVariable, Expression from, string upDown, Expression to, Expression withClause, StatementList stmts)
        {
            LoopVariable = loopVariable;
            InitialValue = from;
            EndingValue = to;
            StepDirection = upDown;
            Step = withClause;
            Statements = stmts;
        }

        public override string Print(int depth)
        {
            return "loop " + LoopVariable + ":" + InitialValue.Print(depth) + " " + StepDirection + " " +
                   EndingValue.Print(depth)
                   + (Step == null ? string.Empty : " with " + Step.Print(depth)) + NewLine(depth)
                   + "{" + Statements.Print(depth + 1) + "}";
        }

        public override void Visit(Visitor v)
        {
            v.VisitLoop(this);
        }
    }
}
