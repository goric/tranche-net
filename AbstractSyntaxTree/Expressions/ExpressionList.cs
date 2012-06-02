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
    public class ExpressionList : Expression
    {
        public bool IsEmpty { get; set; }
        public Expression Head { get; set; }
        public ExpressionList Tail { get; set; }
        public int Length { get; set; }

        public ExpressionList ()
        {
            IsEmpty = true;
            Length = 0;
        }
        public ExpressionList(Expression exp, ExpressionList tail)
        {
            Head = exp;
            Tail = tail;
            Length = Tail.Length + 1;
        }

        public override string Print (int depth)
        {
            if (IsEmpty)
                return "";

            if (Tail.IsEmpty)
                return Head.Print(depth);
            
            return Head.Print(depth) + "," + Tail.Print(depth);
        }

        public override void Visit (Visitor v)
        {
            v.VisitExprList(this);
        }
    }
}
