using Plg.Compiler.AST.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plg.Compiler.AST.Commands
{
    /// <summary>
    /// for
    /// </summary>
    public class ForCommand : ICommand
    {
        public CommandType Type => CommandType.ForStatement;


        public DefineVariableCommand Define { get; set; }

        public Expression Condition { get; set; }

        public ICommand Increment { get; set; }

        public Scope BodyScope { get; set; }

        public void Execute()
        {
            
        }
    }
}
