using Plg.Compiler.AST.Expressions;
using Plg.Compiler.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plg.Compiler.AST.Commands
{
    public class BreakCommand : ICommand
    {
        public CommandType Type => CommandType.Break;


        public TokenKind TokenKind { get; set; }

        /// <summary>
        /// 返回值 return时使用
        /// </summary>
        public List<Expression> ReturnValues { get; set; } = new List<Expression>();

        public void Execute()
        {

        }
    }
}
