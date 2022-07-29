using Plg.Compiler.AST.Expressions;
using Plg.Compiler.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plg.Compiler.AST.Commands
{
    /// <summary>
    /// 变量赋值
    /// </summary>
    public class VariableAssignCommand : ICommand
    {
        public CommandType Type => CommandType.VariableAssign;

        /// <summary>
        /// 被赋值的变量名
        /// </summary>
        public string LeftVariableName { get; set; }

        /// <summary>
        /// 操作符
        /// </summary>
        public TokenKind Operator { get; set; }
        /// <summary>
        /// 右侧表达式
        /// </summary>
        public Expression RightExpression { get; set; }

        public void Execute()
        {


            
            
        }
    }
}
