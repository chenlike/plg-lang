using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plg.Compiler.AST.Commands
{
    public class DefineVariableCommand : ICommand
    {
        public CommandType Type => CommandType.DefineVariable;


        public Variable Variable { get; private set; }
        public DefineVariableCommand(Variable variable)
        {
            Variable = variable;
        }
        
        public void Execute()
        {

        }

        
    }
}
