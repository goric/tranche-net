
namespace SemanticAnalysis
{
    /// <summary>
    /// Class for storing descriptors for formal parameters so actual parameters
    /// can be type checked against them
    /// </summary>
    public class FormalDescriptor : Descriptor
    {
        public string Name { get; private set; }

        public string Modifier { get; set; }

        public override bool IsType { get { return true; } }

        /// <summary>
        /// Takes a type and the index in the function of a parameter
        /// </summary>
        public FormalDescriptor (InternalType type, string name, string modifier) : base(type)
        {
            Name = name;
            Modifier = modifier;
        }
    }
}
