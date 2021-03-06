﻿/*  
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
    public class Bond : StatementList
    {
        public StatementList Statements { get; set; }

        public Bond () { IsEmpty = true; }
        public Bond (StatementList items, StatementList tail)
        {
            IsEmpty = false;
            Statements = items;
            Tail = tail;
        }
        
        public override void Visit (Visitor v)
        {
            v.VisitBond(this);
        }

        public override string Print (int depth)
        {
            if (IsEmpty)
                return string.Empty;

            return "Bond {" + NewLine(depth + 1)
                   + (IsEmpty ? string.Empty : Statements.Print(depth + 1))
                   + NewLine(depth) + "}"
                   + NewLine(depth)
                   + (Tail == null ? string.Empty : Tail.Print(depth));
        }
    }
}
