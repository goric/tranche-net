using System;

namespace SemanticAnalysis
{
    /// <summary>
    /// Class for storing descriptors for actual parameters so we can check
    /// them against the formals for readonly status
    /// </summary>
    public class ActualDescriptor : Descriptor
    {
        public String Name { get; private set; }
        public string Modifier { get; private set; }
        public override bool IsType { get { return true; } }
        public bool IsFromFormal { get; set; }
        public bool IsFormalReadonly { get; set; }

        /// <summary>
        /// Takes a type and the index in the function of a parameter
        /// </summary>
        public ActualDescriptor (InternalType type, string modifier = null, string name = null)
            : base(type)
        {
            Type = type;
            Modifier = modifier;
            Name = name;
        }
    }
}
