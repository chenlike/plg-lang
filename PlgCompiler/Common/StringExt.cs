using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlgCompiler.Common
{
    public static class StringExt
    {

        public static string ReplaceIgnore(this string v)
        {
            return v.Replace(" ", "")
                .Replace("\t", "")
                .Replace("\n", "")
                .Replace("\r", "");
        }
    }
}
