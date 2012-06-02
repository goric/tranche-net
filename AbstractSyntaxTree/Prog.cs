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
    public class Prog : Node
    {
        public DeclarationClass Settings { get; set; }
        public DeclarationClass Deal { get; set; }
        public DeclarationClass Collateral { get; set; }
        public DeclarationClass Securities { get; set; }
        public DeclarationClass CreditPaymentRules { get; set; }
        public DeclarationClass Simulation { get; set; }

        public Prog(DeclarationClass s, DeclarationClass d, DeclarationClass c, DeclarationClass sec, DeclarationClass cpr, DeclarationClass sim)
        {
            Settings = s;
            Deal = d;
            Collateral = c;
            Securities = sec;
            CreditPaymentRules = cpr;
            Simulation = sim;
        }

        public override void Visit (Visitor v)
        {
            v.VisitProgram(this);
        }

        public override string Print (int depth)
        {
            return "Program: "
                + NewLine(depth + 1)
                + Settings.Print(depth + 1)
                + NewLine(depth + 1)
                + Deal.Print(depth + 1)
                + NewLine(depth + 1)
                + Collateral.Print(depth + 1)
                + NewLine(depth + 1)
                + Securities.Print(depth + 1)
                + NewLine(depth + 1)
                + CreditPaymentRules.Print(depth + 1)
                + NewLine(depth + 1)
                + Simulation.Print(depth + 1)
                + NewLine(depth + 1);
        }
    }
}
