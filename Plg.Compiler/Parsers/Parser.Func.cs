using Plg.Compiler.AST;
using Plg.Compiler.AST.Commands;
using Plg.Compiler.AST.Expressions;
using Plg.Compiler.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plg.Compiler.Parsers
{
    public partial class Parser
    {

        /// <summary>
        /// 解析方法
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="leftName"></param>
        /// <returns></returns>
        public CallFuncCommand ParseCallFunc(Scope scope, string leftName)
        {
            // print(xxx,1+2,33)
            // print(xx(2),3)
            var cmd = new CallFuncCommand()
            {
                FuncName = leftName,
                Arguments = new List<Expression>()
            };

            _tokenizer.LookAheadAndSkip(TokenKind.Ignore);
            _tokenizer.NextTokenIs(TokenKind.LeftParenthesis);


            while (_tokenizer.LookAheadAndSkip(TokenKind.Ignore).Kind != TokenKind.RightParenthesis)
            {
                var nextToken = _tokenizer.LookAhead();
                if (nextToken.Kind == TokenKind.Comma)
                {
                    // 逗号说明有多个参数
                    _tokenizer.NextTokenIs(TokenKind.Comma);
                    continue;
                }
                if (nextToken.Kind == TokenKind.Semicolon)
                {
                    _tokenizer.NextTokenIs(TokenKind.Semicolon);
                    break;
                }


                var arg = ParseExpression();
                cmd.Arguments.Add(arg);
            }
            _tokenizer.NextTokenIs(TokenKind.RightParenthesis);
            if (scope != null)
            {
                scope.Commands.Add(cmd);
            }

            return cmd;
        }

    }
}
