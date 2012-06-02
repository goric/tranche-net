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
    /// <summary>
    /// For built in functions that contain special characters. Other built in functions are handled as
    /// normal function calls.
    /// </summary>
    public class SpecialFunction : Expression
    {
        public SpecialFunctions Function { get; set; }
        public Expression Param { get; set; }

        public SpecialFunction(string name, Expression param)
        {
            Param = param;

            switch(name.ToUpper())
            {
                case "PLUSDAY":
                    Function = SpecialFunctions.PLUSDAY;
                    break;
                case "MINUSDAY":
                    Function = SpecialFunctions.MINUSDAY;
                    break;
                case "PLUSMONTH":
                    Function = SpecialFunctions.PLUSMONTH;
                    break;
                case "MINUSMONTH":
                    Function = SpecialFunctions.MINUSMONTH;
                    break;
                case "PLUSYEAR":
                    Function = SpecialFunctions.PLUSYEAR;
                    break;
                case "MINUSYEAR":
                    Function = SpecialFunctions.MINUSYEAR;
                    break;
            }
        }

        public override void Visit(Visitor v)
        {
            v.VisitSpecialFunction(this);
        }

        public override string Print(int depth)
        {
            return string.Format("{0}({1})", Function, Param.Print(depth));
        }
    }

    public enum SpecialFunctions
    {
        PLUSDAY,
        MINUSDAY,
        PLUSMONTH,
        MINUSMONTH,
        PLUSYEAR,
        MINUSYEAR
    }
}
