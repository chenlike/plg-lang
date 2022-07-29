using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plg.Compiler.AST;
using Plg.Compiler.AST.Commands;
using Plg.Compiler.AST.Expressions;
using Plg.Compiler.Lexer;

namespace Plg.Compiler.Parsers
{
    public partial class Parser
    {





        /// <summary>
        /// 解析表达式
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Expression ParseExpression()
        {

            // 1;
            // 1 + 2 * 3;
            // "asd";
            // "aa" + "bb";
            // aaaa + "bbbb";
            // 1 == 2;

            _tokenizer.LookAheadAndSkip(Lexer.TokenKind.Ignore);



            Expression expression = new Expression()
            {
                Items = new List<ExpressionItem>()
            };


            var headToken = _tokenizer.LookAhead();
            
            // 遇到终止 ;  就结束
            while (
                headToken.Kind != TokenKind.Semicolon &&
                headToken.Kind != TokenKind.LeftCurly && 
                headToken.Kind != TokenKind.Comma)
            {


                // 判断括号数量是否闭合
                if(headToken.Kind == TokenKind.RightParenthesis)
                {
                    int leftParenthesisCount = expression.Items
                        .Where(t => t.Type == ExpressionItemType.Parenthesis && t.Token.Kind == TokenKind.LeftParenthesis)
                        .Count();
                    int rightParenthesisCount = expression.Items
                        .Where(t => t.Type == ExpressionItemType.Parenthesis && t.Token.Kind == TokenKind.RightParenthesis)
                        .Count();
                    
                    if (leftParenthesisCount < (rightParenthesisCount + 1))
                    {
                        // 如果左括号比右括号数量少 说明应该结束这个表达式了
                        break;
                    }
                }



                switch (headToken.Kind)
                {
                    // 布尔值
                    case TokenKind.True:
                    case TokenKind.Fasle:
                        expression.Items.Add(new ExpressionItem()
                        {
                            Token = new Token() { Kind = headToken.Kind },
                            Type = ExpressionItemType.Bool,
                            Value = headToken.Value
                        });
                        _tokenizer.NextTokenIs(headToken.Kind);
                        break;
                    // 字符串
                    case TokenKind.Quote:

                        string stringValue = ParseString();
                        expression.Items.Add(new ExpressionItem()
                        {
                            Token = new Token() { Kind  = TokenKind.Quote },
                            Type = ExpressionItemType.String,
                            Value = stringValue
                        });
                        break;
                    // 数字
                    case TokenKind.Number:
                        decimal num = ParseNumber();
                        expression.Items.Add(new ExpressionItem()
                        {
                            Token = new Token() { Kind = TokenKind.Number },
                            Type = ExpressionItemType.Number,
                            Value = num.ToString()
                        });
                        break;
                    // 变量名
                    case TokenKind.Name:

                        // 变量名开头的情况有可能是调用方法

                        _tokenizer.LookAheadAndSkip(TokenKind.Ignore);
                        var nameToken = _tokenizer.LookAhead();
                        
                        string name = nameToken.Value;
                        
                        var tempScope = Scope.CreateScope();
                        if (ParseVariableAssignOrCallFunc(tempScope))
                        {
                            // 解析方法或者 赋值成功了
                            var cmd = tempScope.Commands.FirstOrDefault();
                            expression.Items.Add(new ExpressionItem()
                            {
                                Token = new Token() { Kind = TokenKind.Name, Value = name },
                                Type = ExpressionItemType.Command,
                                Value = name,
                                Command = cmd
                            });
                        }
                        else
                        {

                            string member = "";

                            _tokenizer.LookAheadAndSkip(TokenKind.Ignore);
                            // 判断是否是取成员变量
                            // obj.xxx.sss.zzz
                            if (_tokenizer.LookAhead().Kind == TokenKind.Dot)
                            {
                                
                                var aheadToken = _tokenizer.LookAhead();
                                while (aheadToken.Kind == TokenKind.Dot || aheadToken.Kind == TokenKind.Name)
                                {
                                    if (aheadToken.Kind == TokenKind.Dot)
                                    {
                                        _tokenizer.NextTokenIs(TokenKind.Dot);
                                        member += ".";
                                    }
                                    else
                                    {
                                        var memberToken = _tokenizer.NextTokenIs(TokenKind.Name);
                                        member += memberToken.Value;
                                    }
                                    _tokenizer.LookAheadAndSkip(TokenKind.Ignore);
                                    aheadToken = _tokenizer.LookAhead();
                                }
                            }
                            
                            expression.Items.Add(new ExpressionItem()
                            {
                                Token = new Token() { Kind = TokenKind.Name,Value = name },
                                Type = ExpressionItemType.Variable,
                                Value = name,
                                ObjectMember = member
                            });



                        }


                        break;
                    // 操作符
                    case TokenKind.Add:
                    case TokenKind.Sub:
                    case TokenKind.Mul:
                    case TokenKind.Div:
                    case TokenKind.DoubleEqual:
                    case TokenKind.And:
                    case TokenKind.Or:
                    case TokenKind.GreatThan:
                    case TokenKind.LessThan:
                        expression.Items.Add(new ExpressionItem()
                        {
                            Token = new Token() { Kind = headToken.Kind},
                            Type = ExpressionItemType.Operator,
                            Value = TokenMapper.Map[headToken.Kind]
                        });
                        _tokenizer.NextTokenIs(headToken.Kind);
                        break;
                    // 括号
                    case TokenKind.LeftParenthesis:
                    case TokenKind.RightParenthesis:
                        expression.Items.Add(new ExpressionItem()
                        {
                            Token = new Token() { Kind = headToken.Kind },
                            Type = ExpressionItemType.Parenthesis,
                        });
                        _tokenizer.NextTokenIs(headToken.Kind);
                        break;
                    default:
                        throw new NotImplementedException("表达式无法识别token " + headToken.ToString());
                }

                _tokenizer.LookAheadAndSkip(Lexer.TokenKind.Ignore);
                // 接着往前看一个token
                headToken = _tokenizer.LookAhead();
            }

            expression.ReversePolishNotation();

            
            return expression;
            
        }

    }
}
