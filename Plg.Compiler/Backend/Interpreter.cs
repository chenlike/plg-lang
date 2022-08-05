using Plg.Compiler.AST;
using Plg.Compiler.Backend.Runtimes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plg.Compiler.Backend
{
    public class Interpreter
    {
        Runtime? _top = null;
        public Interpreter()
        {
            _top = Runtime.Create();
        }
        
        public void Run(Scope scope)
        {
            _top?.ExecuteScope(scope);
        }


        

    }
}
