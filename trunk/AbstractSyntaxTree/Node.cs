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
using System;
using System.Text;

using QUT.Gppg;

using SemanticAnalysis;

namespace AbstractSyntaxTree
{
    public abstract class Node
    {
        private const int TAB_SPACES = 4;

        public LexLocation Location { get; set; }
        public virtual InternalType InternalType { get; set; }

        protected String NewLine (int depth)
        {
            var s = new StringBuilder(Environment.NewLine);

            for (int i = 0; i < depth * TAB_SPACES; i++)
                s.Append(' ');

            return s.ToString();
        }

        public virtual String Print (int depth)
        {
            return "";
        }

        public virtual void Visit (Visitor v)
        {
            
        }

        public string CheckNullPrint (Node n, int depth)
        {
            return (n != null) ? n.Print(depth) : string.Empty;
        }
    }
}
