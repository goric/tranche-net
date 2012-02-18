using System;
using System.Collections.Generic;
using System.Linq;

namespace SemanticAnalysis
{
    public class TypeFunction : InternalType
    {
        public override bool IsFunction { get { return true; } }

        public override Type CilType
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsConstructor { get; set; }
        public Scope Scope { get; set; }
        public InternalType ReturnType { get; set; }
        public string Name { get; private set; }

        public Block BodyBlock { get; set; }
        public Block CurrentBlock { get; set; }

        public Dictionary<string, InternalType> Formals;
        public Dictionary<string, InternalType> Locals;

        public TypeFunction(string name)
            : this(false)
        {
            Name = name;
        }

        public TypeFunction(bool isCtor)
        {
            IsConstructor = isCtor;
            Formals = new Dictionary<string, InternalType>();
            Locals = new Dictionary<string, InternalType>();

            BodyBlock = new Block(false);
            CurrentBlock = BodyBlock;
        }

        public void AddFormal (string name, InternalType type)
        {
            Formals.Add(name, type);
        }

        public void AddLocal (string name, InternalType type)
        {
            CurrentBlock.AddLocal(name, type);
        }

        public void AddBlock(bool branchStatement)
        {
            var b = new Block(branchStatement);
            CurrentBlock.AddBlock(b);
            CurrentBlock = b;
        }

        public void LeaveBlock()
        {
            CurrentBlock = CurrentBlock.Parent;
        }
        
        public bool HasLocal(string name)
        {
            var tempBlock = CurrentBlock;
            do
            {
                if (tempBlock.HasLocal(name))
                    return true;
                
                tempBlock = tempBlock.Parent;
            }
            while (tempBlock != null);

            return false;
        }

        public void RegisterReturnStatement()
        {
            CurrentBlock.HasReturnStatement = true;
        }

        public bool AllCodePathsReturn()
        {
            return BodyBlock.AllCodePathsReturn();
        }

        /// <summary>
        /// Checks if the given list of actuals satisfied the type constraints of this method.
        /// Checks to make sure there are the same number of actuals and formals, and that each formal is a
        /// supertype of the given actual
        /// </summary>
        /// <param name="actuals"></param>
        /// <returns></returns>
        public bool AcceptCall(List<ActualDescriptor> actuals)
        {
            var formals = Formals.Values.ToList();
            if (formals.Count != actuals.Count)
                return false;

            return !formals.Where((t, i) => !t.IsSupertype(actuals[i].Type)).Any();
        }

        
        public override string ToString() { return ""; }
    }
}
