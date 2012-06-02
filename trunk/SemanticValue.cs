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

namespace tc
{
    /// <summary>
    /// Struct used in the Parser generator as the TValue input for the ShiftReduceParser base class.
    /// This allows to strongly type both the terminals and the non-terminals in the grammar input file
    /// so each semantic action defined will produce a Node of the proper type.
    /// </summary>
    internal struct SemanticValue
    {
        public Token Token { get; set; }

        public Prog Prog { get; set; }
        public StatementList StatementList { get; set; }
        public Statement Statement { get; set; }
        public ExpressionList ExpressionList { get; set; }
        public Expression Expression { get; set; }
        public Bond Bond { get; set; }
        public CollateralItem CollateralItem { get; set; }
        public InterestRules InterestRules { get; set; }
        public PrincipalRules PrincipalRules { get; set; }
        public InternalRuleList InternalRuleList { get; set; }
        public DeclarationClass DeclarationClass { get; set; }
        public SpecialFunction SpecialFunction { get; set; }
    }
}
