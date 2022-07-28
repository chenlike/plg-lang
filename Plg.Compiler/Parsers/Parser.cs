using Plg.Compiler.AST;
using Plg.Compiler.AST.Commands;
using Plg.Compiler.Lexer;

namespace Plg.Compiler.Parsers
{
    public partial class Parser
    {

        Tokenizer _tokenizer;
        public Parser(string sourceCode)
        {
            _tokenizer = new Tokenizer(sourceCode);
        }




        public void Parse()
        {
            Scope topScope = Scope.CreateTopScope();
            while (_tokenizer.LookAhead().Kind != TokenKind.EOF)
            {
                ParseStatement(topScope);
            }
        }

        public void ParseStatement(Scope scope)
        {
            _tokenizer.LookAheadAndSkip(TokenKind.Ignore);
            var token = _tokenizer.LookAheadAndSkip(TokenKind.Ignore).Kind;
            switch (token)
            {
                case TokenKind.Let:
                    ParseVariaibleAssignment(scope);
                    break;
                case TokenKind.If:
                    ParseIf(scope);
                    break;
            }
        }

           



        
        /// <summary>
        /// 声明语句
        /// </summary>
        public VariableAssignmentCommand ParseVariaibleAssignment(Scope scope)
        {
            /*
             let aa:string = "123";
             */
            _tokenizer.LookAheadAndSkip(TokenKind.Ignore);
            _tokenizer.NextTokenIs(TokenKind.Let);
            _tokenizer.NextTokenIs(TokenKind.Ignore);

            // 解析变量名
            var nameToken = _tokenizer.NextTokenIs(TokenKind.Name);
            _tokenizer.LookAheadAndSkip(TokenKind.Ignore);

            _tokenizer.NextTokenIs(TokenKind.Colon);
            
            Variable variable = new Variable()
            {
                Name = nameToken.Value
            };

            
            switch (_tokenizer.LookAhead().Kind)
            {
                case TokenKind.TypeString:
                    _tokenizer.NextTokenIs(TokenKind.TypeString);
                    variable.Type = VariableType.String;
                    break;
                case TokenKind.TypeNumber:
                    _tokenizer.NextTokenIs(TokenKind.TypeNumber);
                    variable.Type = VariableType.Number;
                    break;
                case TokenKind.TypeBool:
                    _tokenizer.NextTokenIs(TokenKind.TypeBool);
                    variable.Type = VariableType.Bool;
                    break;
            }

            _tokenizer.LookAheadAndSkip(TokenKind.Ignore);
            _tokenizer.NextTokenIs(TokenKind.Equal);
            _tokenizer.LookAheadAndSkip(TokenKind.Ignore);
            variable.Expression = ParseExpression();
            
            
            _tokenizer.NextTokenIs(TokenKind.Semicolon);
            var cmd = new VariableAssignmentCommand(variable);
            scope.Commands.Add(cmd);
            return cmd;
        }


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

            while (_tokenizer.LookAhead().Kind != TokenKind.RightCurly)
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
                    _tokenizer.NextTokenIs(TokenKind.LeftCurly);
                    var elseScope = scope.CreateChildScope();
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


    }
    
}