/*  
 * tranche.NET - a DSL for modeling structured finance products.
 * Copyright (C) 2012 Timothy Goric
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
*/
using AbstractSyntaxTree;
using AbstractSyntaxTree.InternalTypes;
using SemanticAnalysis;
using ILGen;

namespace tc
{
    /// <summary>
    /// This pass just sets up the "classes", which are all of the defined sections
    /// of the language. It is not possible to have user-defined classes in Tranche.
    /// </summary>
    public class FirstPass : Visitor
    {
        protected Node _root;
        protected ScopeManager _scopeMgr;
        protected TypeClass _currentClass;

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
                _scopeMgr.AddMethod(m.Name.ToLower(), m.FuncInfo, globalClass, null, true);
            }
        }

        public void Run()
        {
            _root.Visit(this);
        }

        public override void VisitProgram(Prog n)
        {
            n.Settings.Visit(this);
            n.Deal.Visit(this);
            n.Collateral.Visit(this);
            n.Securities.Visit(this);
            n.CreditPaymentRules.Visit(this);
            n.Simulation.Visit(this);
        }
        public override void VisitSettings(Settings n) { VisitDeclarationClass(n); }
        public override void VisitDeal(Deal n) { VisitDeclarationClass(n); }
        public override void VisitCollateral(Collateral n) { VisitDeclarationClass(n); }
        public override void VisitSecurities(Securities n) { VisitDeclarationClass(n); }
        public override void VisitCreditPaymentRules(CreditPaymentRules n) { VisitDeclarationClass(n); }
        public override void VisitSimulation(Simulation n) { VisitDeclarationClass(n); }

        public override void VisitDeclarationClass(DeclarationClass n)
        {
            var cls = new TypeClass(n.Name);
            _currentClass = cls;
            n.Descriptor = _scopeMgr.AddClass(cls.ClassName, cls);
            n.Type = cls;
        }
    }
}
