using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlgCompiler.Common
{
    public static class Logger
    {
        public static bool Enable { get; set; }
        public static void Log(string content)
        {
            if (Enable)
            {
                Console.WriteLine("[Debug] " + content);
            }
        }
    }
}
