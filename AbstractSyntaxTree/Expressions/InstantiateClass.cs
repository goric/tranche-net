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
using System;
using SemanticAnalysis;

namespace AbstractSyntaxTree
{
    public class InstantiateClass : Expression
    {
        public String ClassName { get; set; }
        public ExpressionList Actuals { get; set; }
        public Descriptor Descriptor { get; set; }
        public Descriptor ClassDescriptor { get; set; }

        public InstantiateClass(String className, ExpressionList actuals)
        {
            ClassName = className;
            Actuals = actuals;
        }

        public override String Print(int depth)
        {
            return " new " + ClassName + "(" + Actuals.Print(depth) + ")";
        }

        public override void Visit(Visitor v)
        {
            v.VisitInstantiateClass(this);
        }
    }
}
