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


        public void Run(Scope scope)
        {
            Runtime runtimeScope = Runtime.Create();
            runtimeScope.ExecuteScope(scope);
        }


        

    }
}
