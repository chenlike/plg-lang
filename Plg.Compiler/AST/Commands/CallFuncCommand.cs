using Plg.Compiler.AST.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plg.Compiler.AST.Commands
{
    public class CallFuncCommand : ICommand
    {
        public CommandType Type =>  CommandType.CallFunc;

        public string FuncName { get; set; }

        public List<Expression> Arguments { get; set; }

        public void Execute()
        {

        }
    }
}
