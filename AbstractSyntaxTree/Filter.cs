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

namespace AbstractSyntaxTree
{
    public class Filter : Statement
    {
        public Identifier SetName { get; set; }
        public Identifier RuleInput { get; set; }
        public string Operation { get; set; }

        public Identifier TempVar { get; set; }
        public Expression Expression { get; set; }

        public Filter(Identifier set, Identifier input, string operation)
        {
            SetName = set;
            RuleInput = input;
            Operation = operation;
        }
        public Filter(Identifier ident, Identifier tmpVar, Expression e)
        {
            SetName = ident;
            TempVar = tmpVar;
            Expression = e;
        }

        public override string Print(int depth)
        {
            if (!string.IsNullOrEmpty(Operation))
                return "filter " + SetName.Print(depth) + " " + RuleInput.Print(depth) + " " + Operation;
            return "filter " + SetName.Print(depth) + " " + TempVar.Print(depth) + " " + Expression.Print(depth);
        }

        public override void Visit(Visitor v)
        {
            v.VisitFilter(this);
        }
    }
}
