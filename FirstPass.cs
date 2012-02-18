using AbstractSyntaxTree;
using SemanticAnalysis;
using ILGen;

namespace tc
{
    public class FirstPass : Visitor
    {
        protected Node _root;
        protected ScopeManager _scopeMgr;

        public FirstPass(Node treeNode, ScopeManager mgr)
        {
            _root = treeNode;
            _scopeMgr = mgr;

            var globalClass = new TypeClass("__global");
            globalClass.Descriptor = _scopeMgr.AddClass(globalClass.ClassName, globalClass);

            //setup built in system methods
            foreach (var m in InternalMethodManager.Methods)
            {
                m.FuncInfo.Scope = _scopeMgr.TopScope;
                _scopeMgr.AddMethod(m.Name, m.FuncInfo, globalClass, null, true);
            }
        }

        public void Run()
        {
            _root.Visit(this);
        }
    }
}
