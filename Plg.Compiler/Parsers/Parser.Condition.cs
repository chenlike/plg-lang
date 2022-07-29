using Plg.Compiler.AST;
using Plg.Compiler.AST.Commands;
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
        /// 解析if语句
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public IfCommand ParseIf(Scope scope)
        {   /*
             if {
            
            }else{  }

            if {
            
            }elif {  }else{  }
            
            

            */

            IfCommand cmd = new IfCommand();

            _tokenizer.LookAheadAndSkip(TokenKind.Ignore);
            #region if开始
            // if
            _tokenizer.NextTokenIs(TokenKind.If);
            _tokenizer.LookAheadAndSkip(TokenKind.Ignore);
            // if 判断
            var ifExpression = ParseExpression();
            // {
            _tokenizer.NextTokenIs(TokenKind.LeftCurly);
            var ifScope = scope.CreateChildScope();
            var ifExpressionScope = new IfExpressionScope()
            {
                Expression = ifExpression,
                Scope = ifScope
            };

            while (_tokenizer.LookAheadAndSkip(TokenKind.Ignore).Kind != TokenKind.RightCurly)
            {
                ParseStatement(ifExpressionScope.Scope);
            }
            // }
            _tokenizer.NextTokenIs(TokenKind.RightCurly);

            cmd.IfExpression = ifExpressionScope;
            #endregion if结束

            // 判断是否有elif 或者 else
            _tokenizer.LookAheadAndSkip(TokenKind.Ignore);
            var nextToken = _tokenizer.LookAhead();
            while (nextToken.Kind == TokenKind.Elif || nextToken.Kind == TokenKind.Else)
            {
                /* 
            if{
            
            }elif {
                
            }else{
                
            }
                 */

                if (nextToken.Kind == TokenKind.Elif)
                {
                    // 识别 elif 
                    _tokenizer.NextTokenIs(TokenKind.Elif);
                    var elifExpr = ParseExpression();
                    _tokenizer.LookAheadAndSkip(TokenKind.Ignore);
                    // {
                    _tokenizer.NextTokenIs(TokenKind.LeftCurly);
                    var elifScope = scope.CreateChildScope();
                    var elifExpressionScope = new IfExpressionScope()
                    {
                        Expression = elifExpr,
                        Scope = elifScope
                    };

                    while (_tokenizer.LookAhead().Kind != TokenKind.RightCurly)
                    {
                        ParseStatement(elifExpressionScope.Scope);
                    }
                    // }

                    cmd.ElifExpressions.Add(elifExpressionScope);
                    _tokenizer.NextTokenIs(TokenKind.RightCurly);
                }
                else if (nextToken.Kind == TokenKind.Else)
                {
                    // 识别 else 
                    _tokenizer.NextTokenIs(TokenKind.Else);
                    _tokenizer.LookAheadAndSkip(TokenKind.Ignore);
                    // {
                    _tokenizer.NextTokenIs(TokenKind.LeftCurly);
                    var elseScope = scope.CreateChildScope();
                    // }
                    while (_tokenizer.LookAhead().Kind != TokenKind.RightCurly)
                    {
                        ParseStatement(elseScope);
                    }
                    cmd.ElseScope = elseScope;
                    _tokenizer.NextTokenIs(TokenKind.RightCurly);
                }
                nextToken = _tokenizer.LookAhead();

            }

            scope.Commands.Add(cmd);
            return cmd;
        }



        public ForCommand ParseFor(Scope scope)
        {
            /*
             for (var i = 0; i < 10; i++)
             {
                 // 循环体
             }
             */
            ForCommand cmd = new ForCommand();

            cmd.BodyScope = scope.CreateChildScope();
            

            _tokenizer.LookAheadAndSkip(TokenKind.Ignore);
            _tokenizer.NextTokenIs(TokenKind.For);
            _tokenizer.LookAheadAndSkip(TokenKind.Ignore);
            _tokenizer.NextTokenIs(TokenKind.LeftParenthesis);
            _tokenizer.LookAheadAndSkip(TokenKind.Ignore);
            // 判断是否有定义语句
            if (_tokenizer.LookAhead().Kind == TokenKind.Semicolon)
            {
                // 没有
                _tokenizer.NextTokenIs(TokenKind.Semicolon);
            }
            else
            {
                cmd.Define = ParseDefineVariaible(cmd.BodyScope);
            }


            // 判断是否有 condition语句
            _tokenizer.LookAheadAndSkip(TokenKind.Ignore);
            if (_tokenizer.LookAhead().Kind == TokenKind.Semicolon)
            {
                // 没有
                _tokenizer.NextTokenIs(TokenKind.Semicolon);
            }
            else
            {
                cmd.Condition = ParseExpression();
                _tokenizer.NextTokenIs(TokenKind.Semicolon);
            }
            

            _tokenizer.LookAheadAndSkip(TokenKind.Ignore);
            // 判断是否有 increment语句
            if (_tokenizer.LookAhead().Kind == TokenKind.Semicolon)
            {
                // 没有
                _tokenizer.NextTokenIs(TokenKind.Semicolon);
            }
            else
            {
                var tempScope = Scope.CreateScope();
                if (ParseVariableAssignOrCallFunc(tempScope))
                {
                    cmd.Increment = tempScope.Commands.FirstOrDefault();
                }
            }

            // ) 
            _tokenizer.LookAheadAndSkip(TokenKind.Ignore);
            _tokenizer.NextTokenIs(TokenKind.RightParenthesis);
            _tokenizer.LookAheadAndSkip(TokenKind.Ignore);
            // {
            _tokenizer.NextTokenIs(TokenKind.LeftCurly);

            while (_tokenizer.LookAheadAndSkip(TokenKind.Ignore).Kind != TokenKind.RightCurly)
            {
                ParseStatement(cmd.BodyScope);
            }
            
            _tokenizer.NextTokenIs(TokenKind.RightCurly);

            scope.Commands.Add(cmd);
            return cmd;
        }


    }
}
