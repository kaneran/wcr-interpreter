using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcr_interpreter
{
    public static class Constants
    {
        public static class Precdence
        {
            public static string LOWEST = "LOWEST";
            public static string EQUALS = "EQUALS";
            public static string LESSGREATER = "LESSGREATER";
            public static string SUM = "SUM";
            public static string PRODUCT = "PRODUCT";
            public static string PREFIX = "PREFIX";
            public static string CALL = "CALL";
        }

        public static string[] Precedence = new string[]
        {
            Precdence.LOWEST,
            Precdence.EQUALS,
            Precdence.LESSGREATER,
            Precdence.SUM,
            Precdence.PRODUCT,
            Precdence.PREFIX,
            Precdence.CALL
        };
    }
}
