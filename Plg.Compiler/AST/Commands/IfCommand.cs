using Plg.Compiler.AST.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plg.Compiler.AST.Commands
{
    public class IfExpressionScope
    {
        public Expression Expression { get; set; }
        public Scope Scope { get; set; }
    }
    public class IfCommand : ICommand
    {
        public CommandType Type =>  CommandType.IfStatement;



        public IfExpressionScope IfExpression { get; set; }
        public List<IfExpressionScope> ElifExpressions { get; set; } = new List<IfExpressionScope>();
        public Scope ElseScope { get; set; }

        public void Execute()
        {

        }
    }
}
