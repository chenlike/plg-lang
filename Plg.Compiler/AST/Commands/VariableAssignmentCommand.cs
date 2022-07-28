using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plg.Compiler.AST.Commands
{
    public class VariableAssignmentCommand : ICommand
    {
        public CommandType Type => CommandType.VariableAssignment;


        public Variable Variable { get; private set; }
        public VariableAssignmentCommand(Variable variable)
        {
            Variable = variable;
        }
        
        public void Execute()
        {

        }

        
    }
}
