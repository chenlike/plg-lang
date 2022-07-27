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
        /// 解析字符串
        /// </summary>
        /// <returns></returns>
        public string ParseString()
        {
            _tokenizer.LookAheadAndSkip(Lexer.TokenKind.Ignore);
            _tokenizer.NextTokenIs(TokenKind.Quote);
            string value = _tokenizer.ScanContentBeforeToken(TokenKind.Quote);
            _tokenizer.NextTokenIs(TokenKind.Quote);
            return value;
        }
        
        /// <summary>
        /// 解析数字
        /// </summary>
        /// <returns></returns>
        public decimal ParseNumber()
        {
            _tokenizer.LookAheadAndSkip(Lexer.TokenKind.Ignore);
            var token = _tokenizer.NextTokenIs(TokenKind.Number);
            return decimal.Parse(token.Value);
        }


        
        
    }
}
