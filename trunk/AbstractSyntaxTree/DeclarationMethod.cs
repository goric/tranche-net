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
using SemanticAnalysis;

namespace AbstractSyntaxTree
{
    public class DeclarationMethod : Node
    {
        public InternalType ReturnType { get; set; }
        public String Name { get; set; }
        public StatementList Body { get; set; }
        public InternalType Type { get; set; }
        public MethodDescriptor Descriptor { get; set; }

        public DeclarationMethod(String name, StatementList body)
        {
            ReturnType = new TypeVoid();
            Name = name;
            Body = body;
        }

        public override String Print(int depth)
        {
            return ReturnType.Print(depth) + " " + Name + "(" + ")" + "{" + NewLine(depth + 1)
                + (Body == null ? string.Empty : Body.Print(depth + 1)) + NewLine(depth) + "}";
        }
    }
}
