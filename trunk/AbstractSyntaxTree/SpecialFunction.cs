using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
