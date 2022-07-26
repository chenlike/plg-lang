using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlgCompiler.Tests.LexerTests
{
    public class TokenizerTests
    {
        [Test]
        public void TokenInMapper()
        {
            foreach(var item in TokenMapper.Map)
            {
                Tokenizer tokenizer = new Tokenizer(item.Value);
                tokenizer.NextTokenIs(item.Key);
            }
        }

        [Test]
        public void LookAheadAndSkip()
        {
            Tokenizer tokenizer = new Tokenizer("  +");
            var first = tokenizer.LookAheadAndSkip(TokenKind.Ignore);
            tokenizer.NextTokenIs(TokenKind.Add);
        }
    }
}
