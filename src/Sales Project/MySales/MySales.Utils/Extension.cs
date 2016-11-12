using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySales.Utils
{
    public static class Extension
    {
        public static string ToCamelCase(this string st)
        {
            if (st == "ID")
                return "Id";
            return st;
        }
    }
}
