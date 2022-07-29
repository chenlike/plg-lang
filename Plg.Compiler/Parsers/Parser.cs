using Plg.Compiler.AST;
using Plg.Compiler.AST.Commands;
using Plg.Compiler.AST.Expressions;
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
            Scope topScope = Scope.CreateScope();
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
                    // 变量定义语句
                    ParseDefineVariaible(scope);
                    break;
                case TokenKind.Name:
                    // name 开头的 可能是赋值语句或者调用方法
                    ParseVariableAssignOrCallFunc(scope);
                    _tokenizer.LookAheadAndSkip(TokenKind.Semicolon);
                    break;
                case TokenKind.If:
                    ParseIf(scope);
                    break;
                case TokenKind.For:
                    ParseFor(scope);
                    break;
                case TokenKind.Break:
                case TokenKind.Continue:
                case TokenKind.Return:
                    ParseBreak(scope);
                    break;

            }
        }

           



        /// <summary>
        /// 中断语句
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public BreakCommand ParseBreak(Scope scope)
        {
            _tokenizer.LookAheadAndSkip(TokenKind.Ignore);

            var token = _tokenizer.LookAhead();

            BreakCommand cmd = new BreakCommand();

            cmd.TokenKind = token.Kind;
            _tokenizer.NextTokenIs(token.Kind);
            if (token.Kind == TokenKind.Return)
            {
                // 尝试读取返回值
                _tokenizer.LookAheadAndSkip(TokenKind.Ignore);

                while (_tokenizer.LookAheadAndSkip(TokenKind.Ignore).Kind != TokenKind.Semicolon)
                {
                    _tokenizer.LookAheadAndSkip(TokenKind.Comma);

                    var expr = ParseExpression();
                    cmd.ReturnValues.Add(expr);
                }

            }

            
            _tokenizer.LookAheadAndSkip(TokenKind.Ignore);
            _tokenizer.NextTokenIs(TokenKind.Semicolon);
            scope.Commands.Add(cmd);
            return cmd;
        }

        
        /// <summary>
        /// 变量声明语句
        /// </summary>
        public DefineVariableCommand ParseDefineVariaible(Scope scope)
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
            _tokenizer.LookAheadAndSkip(TokenKind.Ignore);
            _tokenizer.NextTokenIs(TokenKind.Semicolon);

            var cmd = new DefineVariableCommand(variable);
            scope.Commands.Add(cmd);
            return cmd;
        }


        /// <summary>
        /// 解析赋值语句或者 调用方法
        /// </summary>
        /// <param name="scope"></param>
        public bool ParseVariableAssignOrCallFunc(Scope scope)
        {
            _tokenizer.LookAheadAndSkip(TokenKind.Ignore);
            var name = _tokenizer.NextTokenIs(TokenKind.Name);

            _tokenizer.LookAheadAndSkip(TokenKind.Ignore);
            bool parsed = false;

            switch (_tokenizer.LookAhead().Kind)
            {
                case TokenKind.LeftParenthesis:
                    // 调用函数
                    parsed = true;
                    ParseCallFunc(scope, name.Value);
                    break;
                case TokenKind.Increase:
                case TokenKind.Decrease:
                case TokenKind.AddEqual:
                case TokenKind.SubEqual:
                case TokenKind.MulEqual:
                case TokenKind.DivEqual:
                case TokenKind.Equal:
                    parsed = true;
                    // 变量赋值
                    ParseVariableAssign(scope, name.Value);
                    break;
            }
            return parsed;
        }


        /// <summary>
        /// 解析变量赋值语句
        /// </summary>
        public VariableAssignCommand ParseVariableAssign(Scope scope,string leftName)
        {



            // 变量赋值语句

            // a++; a--;
            // a+=1 a -=1;
            // a *= ab +2;
            // a = b;


            VariableAssignCommand cmd = new VariableAssignCommand()
            {
                LeftVariableName = leftName,
                
            };

            _tokenizer.LookAheadAndSkip(TokenKind.Ignore);
            var lookAhead = _tokenizer.LookAhead();
            switch (lookAhead.Kind)
            {

                case TokenKind.Increase:
                case TokenKind.Decrease:

                    _tokenizer.NextTokenIs(lookAhead.Kind);
                    // ++ 和 -- 没有右侧表达式
                    cmd.Operator = lookAhead.Kind;
                    break;


                case TokenKind.AddEqual:
                case TokenKind.SubEqual:
                case TokenKind.MulEqual:
                case TokenKind.DivEqual:
                case TokenKind.Equal:
                    // += -= *= /= =

                    // 都需要右侧表达式
                    _tokenizer.NextTokenIs(lookAhead.Kind);
                    cmd.Operator = lookAhead.Kind;

                    
                    _tokenizer.LookAheadAndSkip(TokenKind.Ignore);
                    cmd.RightExpression = ParseExpression();
                    break;

                default:
                    break;
            }

            // 吃掉分号
            _tokenizer.LookAheadAndSkip(TokenKind.Ignore);

            if(scope != null)
            {
                scope.Commands.Add(cmd);
            }

            return cmd;
        }














    }

}