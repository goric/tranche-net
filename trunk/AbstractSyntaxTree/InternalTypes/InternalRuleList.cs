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

namespace AbstractSyntaxTree.InternalTypes
{
    public class InternalRuleList : ExpressionList
    {
        public string Joiner { get; set; }

        public InternalRuleList () { IsEmpty = true; }
        public InternalRuleList (Expression head, ExpressionList tail)
        {
            Head = head;
            Tail = tail;
            IsEmpty = false;
        }

        public override void Visit (Visitor v)
        {
            v.VisitInternalRuleList(this);
        }

        public override string Print (int depth)
        {
            if (IsEmpty)
                return string.Empty;
            return "->" + Head.Print(depth)
                + (Tail.IsEmpty || Tail is InternalRuleListAnd || Tail is InternalRuleListOr ? string.Empty : NewLine(depth))
                + Tail.Print(depth);
        }
    }
}
