using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlgCompiler.Lexer
{
    public class Token
    {

        /// <summary>
        /// token 类型
        /// </summary>
        public TokenKind Kind { get; private set; }
        /// <summary>
        /// 字符
        /// </summary>
        public string Value { get { return TokenMapper.Map[Kind]; } }

        public Token(TokenKind kind)
        {
            Kind = kind;
        }




    }
}
