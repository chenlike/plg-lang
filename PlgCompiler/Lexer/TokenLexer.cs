using PlgCompiler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlgCompiler.Lexer
{
    public class TokenLexer
    {

        /// <summary>
        /// 源代码 用于读取
        /// </summary>
        StringBuilder _sourceCode;
        
        /// <summary>
        /// 源代码 原始 
        /// </summary>
        string _rawSourceCode;



        int _currentPos;
        int _lineNum;




        Token? _nextToken = null;
        
        
        public TokenLexer(string sourceCode)
        {
            _sourceCode = new StringBuilder(sourceCode);
            _rawSourceCode = sourceCode;
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



        public void NextTokenIs(TokenKind kind)
        {
            var token = LookAheadAndSkip(kind);
            if(token != kind)
            {
                throw new Exception("下一个token类型错误");
            }
        }

        /// <summary>
        /// 匹配token
        /// </summary>
        /// <returns></returns>
        public Token MatchToken()
        {
            if (_nextToken != null)
            {
                var nextResult = _nextToken;
                _nextToken = null;
                return nextResult;
            }


            if (skipIgnore())
            {
                Logger.Log("读取token " + TokenKind.Ignore.ToString());
                return new Token(TokenKind.Ignore);
            }

            if (_sourceCode.Length == 0)
            {
                Logger.Log("读取token " + TokenKind.Ignore.ToString());
                return new Token(TokenKind.EOF);
            }

            Token? result = null;

            string currentSouceCode = _sourceCode.ToString();
            foreach(var tokenItem in TokenMapper.Map.Where(t=>t.Key != TokenKind.EOF && t.Key != TokenKind.Ignore))
            {
                if(currentSouceCode.StartsWith(tokenItem.Value)){
                    result = new Token(tokenItem.Key);
                    break; 
                }
            }

            if(result == null)
            {
                throw new Exception();
            }

            skipSouceCode(result.Value.Length);
            Logger.Log("读取token " + result.Kind.ToString());
            return result;
        }

        /// <summary>
        /// 向前看一个token
        /// </summary>
        /// <returns></returns>
        public TokenKind LookAhead()
        {
            if (_nextToken != null)
            {
                return _nextToken.Kind;
            }
            var nextToken = MatchToken();
            _nextToken = nextToken;
            return _nextToken.Kind;
        }
        /// <summary>
        /// 向前看一个token 并且如果是指定类型的话 吃掉他
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public TokenKind LookAheadAndSkip(TokenKind token)
        {
            var aheadToken = LookAhead();
            if(aheadToken == token)
            {
                // 吃掉这个token
                MatchToken();
                return aheadToken;
            }

            return aheadToken;
        }

        /// <summary>
        /// 跳过忽略字符
        /// </summary>
        /// <returns></returns>
        public bool skipIgnore()
        {
            bool isIgnore = false;
            while (_sourceCode.Length > 0)
            {
                char c = _sourceCode[0];

                if (c == '\t' || c == ' ')
                {
                    skipSouceCode(1);
                    isIgnore = true;
                } else if (c == '\n' || c == '\r') {
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

        
        void skipSouceCode(int len)
        {
            _sourceCode.Remove(0, len);
            _currentPos += len;
            Logger.Log("SourceCode Skip:" + len);
        }

        

        
        

    }
}
