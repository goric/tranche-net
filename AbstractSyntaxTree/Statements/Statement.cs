namespace AbstractSyntaxTree
{
    public class Statement : Node
    {
        public override void Visit (Visitor v)
        {
            v.VisitStatement(this);
        }

        /// <summary>
        /// Takes a statement, and if it's not already a block, wraps it in an Block.
        /// </summary>
        /// <returns></returns>
        public Block WrapInBlock()
        {
            return this is Block ? (Block) this : new Block(this);
        }
    }
}
