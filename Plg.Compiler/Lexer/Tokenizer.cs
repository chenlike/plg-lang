using Plg.Compiler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Plg.Compiler.Lexer
{
    public class Tokenizer
    {

        string _sourceCode;
        string _rawCode;

        int _currentPos = 0;
        int _lineNum = 1;



        Token? _nextToken = null;

        
        public Tokenizer(string sourceCode)
        {
            _sourceCode = sourceCode;
            _rawCode = sourceCode;
            _currentPos = 0;
            _lineNum = 1;
        }


        /// <summary>
        /// 扫描某个token前的所有内容
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public string ScanContentBeforeToken(TokenKind token)
        {
            string currentSouceCode = _sourceCode.ToString();

            int pos = currentSouceCode.IndexOf(TokenMapper.Map[token]);
            if (pos != -1)
            {
                string value = currentSouceCode.Substring(0, pos);
                skipSouceCode(value.Length);
                return value;
            }

            throw new Exception("没有找到下一个token");
        }



        public Token NextTokenIs(TokenKind kind)
        {
            var token = LookAheadAndSkip(kind);
            if (token.Kind != kind)
            {
                throw new Exception("下一个token类型错误");
            }
            return token;
        }
        /// <summary>
        /// 向前看一个token
        /// </summary>
        /// <returns></returns>
        public Token LookAhead()
        {
            if (_nextToken != null)
            {
                return _nextToken;
            }
            var nextToken = matchToken();
            _nextToken = nextToken;
            return _nextToken;
        }
        /// <summary>
        /// 向前看一个token 并且如果是指定类型的话 吃掉他
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Token LookAheadAndSkip(TokenKind token)
        {
            var aheadToken = LookAhead();
            if (aheadToken.Kind == token)
            {
                // 吃掉这个token
                matchToken();
                return aheadToken;
            }

            return aheadToken;
        }

        

        /// <summary>
        /// 匹配token
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private Token matchToken()
        {
            if (_nextToken != null)
            {
                var nextResult = _nextToken;
                _nextToken = null;
                return nextResult;
            }

            if (skipIgnore())
            {
                return new Token()
                {
                    Kind = TokenKind.Ignore,
                    Value = ""
                };
            }

            if(_sourceCode.Length == 0)
            {
                return new Token()
                {
                    Kind = TokenKind.EOF,
                    Value = ""
                };
            }


            
            var token = _sourceCode switch
            {
                string t when t.StartsWith(";") => new Token() { Kind = TokenKind.Semicolon,Value = ";" },
                string t when t.StartsWith(":") => new Token() { Kind = TokenKind.Colon, Value = ":" },
                

                string t when t.StartsWith("==") => new Token() { Kind = TokenKind.DoubleEqual, Value = "==" },
                string t when t.StartsWith("=") => new Token() { Kind = TokenKind.Equal, Value = "=" },
                string t when t.StartsWith("\"") => new Token() { Kind = TokenKind.Quote, Value = "\"" },
                string t when t.StartsWith(",") => new Token() { Kind = TokenKind.Comma, Value = "," },
                string t when t.StartsWith("{") => new Token() { Kind = TokenKind.LeftCurly, Value = "{" },
                string t when t.StartsWith("}") => new Token() { Kind = TokenKind.RightCurly, Value = "}" },
                string t when t.StartsWith("[") => new Token() { Kind = TokenKind.LeftBracket, Value = "[" },
                string t when t.StartsWith("]") => new Token() { Kind = TokenKind.RightBracket, Value = "]" },
                string t when t.StartsWith("(") => new Token() { Kind = TokenKind.LeftParenthesis, Value = "(" },
                string t when t.StartsWith(")") => new Token() { Kind = TokenKind.RightParenthesis, Value = ")" },

                string t when t.StartsWith("&&") => new Token() { Kind = TokenKind.And, Value = "&&" },
                string t when t.StartsWith("||") => new Token() { Kind = TokenKind.Or, Value = "||" },


                string t when t.StartsWith("+=") => new Token() { Kind = TokenKind.AddEqual, Value = "+=" },
                string t when t.StartsWith("-=") => new Token() { Kind = TokenKind.SubEqual, Value = "-=" },
                string t when t.StartsWith("*=") => new Token() { Kind = TokenKind.MulEqual, Value = "*=" },
                string t when t.StartsWith("/=") => new Token() { Kind = TokenKind.DivEqual, Value = "/=" },

                string t when t.StartsWith("++") => new Token() { Kind = TokenKind.Increase, Value = "++" },
                string t when t.StartsWith("--") => new Token() { Kind = TokenKind.Decrease, Value = "--" },

                string t when t.StartsWith("+") => new Token() { Kind = TokenKind.Add, Value = "+" },
                string t when t.StartsWith("-") => new Token() { Kind = TokenKind.Sub, Value = "-" },
                string t when t.StartsWith("*") => new Token() { Kind = TokenKind.Mul, Value = "*" },
                string t when t.StartsWith("/") => new Token() { Kind = TokenKind.Div, Value = "/" },


                
                string t when t.StartsWith(">") => new Token() { Kind = TokenKind.GreatThan, Value = ">" },
                string t when t.StartsWith("<") => new Token() { Kind = TokenKind.LessThan, Value = "<" },



                string t when new Regex(@"^let(?![_A-Za-z0-9])").IsMatch(t) => new Token() { Kind = TokenKind.Let, Value = "let" },
                string t when new Regex(@"^fn(?![_A-Za-z0-9])").IsMatch(t) => new Token() { Kind = TokenKind.Fn, Value = "fn" },
                string t when new Regex(@"^if(?![_A-Za-z0-9])").IsMatch(t) => new Token() { Kind = TokenKind.If, Value = "if" },
                string t when new Regex(@"^elif(?![_A-Za-z0-9])").IsMatch(t) => new Token() { Kind = TokenKind.Elif, Value = "elif" },
                string t when new Regex(@"^else(?![_A-Za-z0-9])").IsMatch(t) => new Token() { Kind = TokenKind.Else, Value = "else" },
                string t when new Regex(@"^for(?![_A-Za-z0-9])").IsMatch(t) => new Token() { Kind = TokenKind.For, Value = "for" },

                string t when new Regex(@"^string(?![_A-Za-z0-9])").IsMatch(t) => new Token() { Kind = TokenKind.TypeString, Value = "string" },
                string t when new Regex(@"^number(?![_A-Za-z0-9])").IsMatch(t) => new Token() { Kind = TokenKind.TypeNumber, Value = "number" },
                string t when new Regex(@"^bool(?![_A-Za-z0-9])").IsMatch(t) => new Token() { Kind = TokenKind.TypeBool, Value = "bool" },
                string t when new Regex(@"^plg(?![_A-Za-z0-9])").IsMatch(t) => new Token() { Kind = TokenKind.TypePlg, Value = "plg" },

                string t when new Regex(@"^true(?![_A-Za-z0-9])").IsMatch(t) => new Token() { Kind = TokenKind.True, Value = "true" },
                string t when new Regex(@"^false(?![_A-Za-z0-9])").IsMatch(t) => new Token() { Kind = TokenKind.Fasle, Value = "false" },

                string t when new Regex(@"^[_A-Za-z][_0-9A-Za-z]*").Match(t) is var match && match.Success => new Token() { Kind = TokenKind.Name, Value = match.Value },
                string t when new Regex(@"^-?\d+(\.\d{1,2})?").Match(t) is var match && match.Success => new Token() { Kind = TokenKind.Number, Value = match.Value },

                _ => throw new NotImplementedException(),
            };
            skipSouceCode(token.Value.Length);

            return token;
        }

        private bool skipIgnore()
        {
            bool isIgnore = false;
            while (_sourceCode.Length > 0)
            {
                char c = _sourceCode[0];

                if (c == '\t' || c == ' ')
                {
                    skipSouceCode(1);
                    isIgnore = true;
                }
                else if (c == '\n' || c == '\r')
                {
                    skipSouceCode(1);
                    _lineNum++;
                    isIgnore = true;
                }
                else
                {
                    break;
                }
            }
            return isIgnore;
        }


        private void skipSouceCode(int len)
        {
            _sourceCode = _sourceCode.Remove(0, len);
            _currentPos += len;
            Logger.Log("SourceCode Skip:" + len);
        }


    }
}
