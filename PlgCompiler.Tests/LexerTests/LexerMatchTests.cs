using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlgCompiler.Tests.LexerTests
{
    public class LexerMatchTests
    {
        
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void MatchToken()
        {

            foreach(var item in TokenMapper.Map)
            {
                if(item.Key == TokenKind.EOF || item.Key == TokenKind.Ignore)
                {
                    continue;
                }
                string value = item.Value;

                Lexer.TokenLexer lexer = new Lexer.TokenLexer(value);
                var token = lexer.MatchToken();
                Assert.IsTrue(token.Kind == item.Key,$"{item.Key} token匹配失败");
            }

        }

        /// <summary>
        /// 匹配token  
        /// </summary>
        [Test]
        public void MatchTokenWithIgnore()
        {

            foreach (var item in TokenMapper.Map)
            {
                if (item.Key == TokenKind.EOF || item.Key == TokenKind.Ignore)
                {
                    continue;
                }
                string value = "   \t \r\n  \r  \n  " + item.Value;

                Lexer.TokenLexer lexer = new Lexer.TokenLexer(value);
                var ignoreToken = lexer.MatchToken();
                Assert.IsTrue(ignoreToken.Kind == TokenKind.Ignore, $"{item.Key} IgnoreToken匹配失败");
                var token = lexer.MatchToken();
                Assert.IsTrue(token.Kind == item.Key, $"{item.Key} token匹配失败");
            }

        }


        [Test]
        public void ScanContentBeforeToken()
        {
            Lexer.TokenLexer lexer = new Lexer.TokenLexer("asdasd :");
            string v = lexer.ScanContentBeforeToken(TokenKind.Colon);
            Assert.IsTrue("asdasd " == v);
            lexer.NextTokenIs(TokenKind.Colon);
        }


    }
}
