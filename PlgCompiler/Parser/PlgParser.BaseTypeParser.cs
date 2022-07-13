using PlgCompiler.Common;
using PlgCompiler.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlgCompiler.Parser
{
    public partial class PlgParser
    {
        /// <summary>
        /// 解析字符串
        /// </summary>
        /// <returns></returns>
        public string ParseString()
        {
            _lexer.LookAheadAndSkip(TokenKind.Ignore);

            _lexer.NextTokenIs(TokenKind.Quote);
            string value = _lexer.ScanContentBeforeToken(TokenKind.Quote);
            _lexer.NextTokenIs(TokenKind.Quote);

            return value;
        }

        /// <summary>
        /// 解析数字
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public decimal ParseNumber()
        {
            _lexer.LookAheadAndSkip(TokenKind.Ignore);

            string value = _lexer.ScanContentBeforeToken(TokenKind.Semicolon).ReplaceIgnore();

            if (decimal.TryParse(value,out decimal v))
            {
                return v;
            }
            throw new NotImplementedException("number 格式错误 " + value);
        }
        /// <summary>
        /// 解析bool
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool ParseBool()
        {
            _lexer.LookAheadAndSkip(TokenKind.Ignore);

            string value = _lexer.ScanContentBeforeToken(TokenKind.Semicolon).ReplaceIgnore();

            if (bool.TryParse(value, out bool v))
            {
                return v;
            }
            throw new NotImplementedException("bool 格式错误 " + value);
        }


        

    }
}
