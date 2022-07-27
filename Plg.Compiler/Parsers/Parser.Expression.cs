using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            _tokenizer.LookAheadAndSkip(Lexer.TokenKind.Ignore);



            Expression expression = new Expression()
            {
                Items = new List<ExpressionItem>()
            };


            var headToken = _tokenizer.LookAhead();
            
            // 遇到终止 ;  就结束
            while (headToken.Kind != TokenKind.Semicolon)
            {

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
                        var nameToken = _tokenizer.NextTokenIs(TokenKind.Name);
                        expression.Items.Add(new ExpressionItem()
                        {
                            Token = nameToken,
                            Type = ExpressionItemType.Variable,
                            Value = nameToken.Value
                        });
                        break;
                    // 操作符
                    case TokenKind.Add:
                    case TokenKind.Sub:
                    case TokenKind.Mul:
                    case TokenKind.Div:
                        expression.Items.Add(new ExpressionItem()
                        {
                            Token = new Token() { Kind = headToken.Kind},
                            Type = ExpressionItemType.Operator,
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
