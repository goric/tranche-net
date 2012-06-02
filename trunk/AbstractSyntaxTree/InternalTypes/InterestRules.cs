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
using SemanticAnalysis;

namespace AbstractSyntaxTree.InternalTypes
{
    public class InterestRules : StatementList
    {
        public Expression Statements { get; set; }

        public InterestRules () { }
        public InterestRules (Expression stmt)
        {
            Statements = stmt;
        }
        
        public override void Visit (Visitor v)
        {
            v.VisitInterestRules(this);
        }

        public override string Print (int depth)
        {
            if (Statements == null)
                return string.Empty;
            return "Interest {" + NewLine(depth + 1) + Statements.Print(depth + 1) + NewLine(depth) + "}" + NewLine(depth);
        }
    }
}
