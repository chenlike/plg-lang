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
        /// 解析方法调用
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


        /// <summary>
        /// 解析方法
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public DefineFuncCommand ParseFunc(Scope scope)
        {
            /*
            fn sa(a:string,b:number) -> (number,string) {


            }
            
            fn sa(a:string,b:number){


            }
             */

            _tokenizer.LookAheadAndSkip(TokenKind.Ignore);

            _tokenizer.NextTokenIs(TokenKind.Fn);
            _tokenizer.NextTokenIs(TokenKind.Ignore);

            var cmd = new DefineFuncCommand();

            var nameToken = _tokenizer.NextTokenIs(TokenKind.Name);
            cmd.FuncName = nameToken.Value;

            _tokenizer.LookAheadAndSkip(TokenKind.Ignore);
            // (
            _tokenizer.NextTokenIs(TokenKind.LeftParenthesis);

            while(_tokenizer.LookAheadAndSkip( TokenKind.Ignore).Kind != TokenKind.RightParenthesis)
            {
                // 跳过逗号
                _tokenizer.LookAheadAndSkip(TokenKind.Comma);
                _tokenizer.LookAheadAndSkip(TokenKind.Ignore);

                // a
                var argNameToken = _tokenizer.NextTokenIs(TokenKind.Name);
                _tokenizer.LookAheadAndSkip(TokenKind.Ignore);
                // :
                _tokenizer.NextTokenIs(TokenKind.Colon);
                
                _tokenizer.LookAheadAndSkip(TokenKind.Ignore);
                var nextToken = _tokenizer.LookAhead();

                FuncArgument argument = new FuncArgument()
                {
                    Name = argNameToken.Value,
                };

                switch (nextToken.Kind)
                {
                    case TokenKind.TypeString:
                        argument.Type = VariableType.String;
                        break;
                    case TokenKind.TypeNumber:
                        argument.Type = VariableType.Number;
                        break;
                    case TokenKind.TypeBool:
                        argument.Type = VariableType.Bool;
                        break;
                    case TokenKind.TypePlg:
                        argument.Type = VariableType.Plg;
                        break;
                    default:
                        throw new NotImplementedException("未实现的类型 "+nextToken.Kind.ToString());
                }
                _tokenizer.NextTokenIs(nextToken.Kind);
                cmd.Arguments.Add(argument);
                
            }
            _tokenizer.LookAheadAndSkip(TokenKind.Ignore);
            // )
            _tokenizer.NextTokenIs(TokenKind.RightParenthesis);

            _tokenizer.LookAheadAndSkip(TokenKind.Ignore);


            if(_tokenizer.LookAhead().Kind == TokenKind.Arrow)
            {
                // ->
                _tokenizer.NextTokenIs(TokenKind.Arrow);
                _tokenizer.LookAheadAndSkip(TokenKind.Ignore);

                // 读取返回参数
                _tokenizer.NextTokenIs(TokenKind.LeftParenthesis);
                while (_tokenizer.LookAheadAndSkip(TokenKind.Ignore).Kind != TokenKind.RightParenthesis)
                {
                    // 跳过逗号
                    _tokenizer.LookAheadAndSkip(TokenKind.Comma);

                    _tokenizer.LookAheadAndSkip(TokenKind.Ignore);
                    var nextToken = _tokenizer.LookAhead();
                    switch (nextToken.Kind)
                    {
                        case TokenKind.TypeString:
                            cmd.ReturnValues.Add(VariableType.String);
                            break;
                        case TokenKind.TypeNumber:
                            cmd.ReturnValues.Add(VariableType.Number);
                            break;
                        case TokenKind.TypeBool:
                            cmd.ReturnValues.Add(VariableType.Bool);
                            break;
                        case TokenKind.TypePlg:
                            cmd.ReturnValues.Add(VariableType.Plg);
                            break;
                        default:
                            throw new NotImplementedException("未实现的类型 " + nextToken.Kind.ToString());
                    }
                    _tokenizer.NextTokenIs(nextToken.Kind);
                }
                _tokenizer.NextTokenIs(TokenKind.RightParenthesis);
            }

            _tokenizer.LookAheadAndSkip(TokenKind.Ignore);
            // {
            _tokenizer.NextTokenIs(TokenKind.LeftCurly);


            cmd.BodyScope = scope.CreateChildScope();

            // 读取方法体内容

            while (_tokenizer.LookAhead().Kind != TokenKind.RightCurly)
            {
                ParseStatement(cmd.BodyScope);
            }

            // }
            _tokenizer.NextTokenIs(TokenKind.RightCurly);


            scope.Commands.Add(cmd);
            
            return cmd;
        }

    }
}
