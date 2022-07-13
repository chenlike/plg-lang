using PlgCompiler.Common;
using PlgCompiler.Definitions;
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


        string _sourceCode;
        TokenLexer _lexer;

        public PlgParser(string sourceCode)
        {
            _sourceCode = sourceCode;
            _lexer = new TokenLexer(sourceCode);
        }




        public void ParseStatements()
        {
            List<Statement> statements = new List<Statement>();

            while (_lexer.LookAhead() != TokenKind.EOF)
            {
                statements.Add(ParseStatement());
            }
        }

        /// <summary>
        /// 解析语句
        /// </summary>
        public Statement ParseStatement()
        {
            _lexer.LookAheadAndSkip(TokenKind.Ignore);
            var nextToken = _lexer.LookAhead();
            switch (nextToken)
            {

                case TokenKind.Let:
                    ParseAssignment();
                    break;
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// 解析定义变量语句
        /// </summary>
        public Variable ParseAssignment()
        {
            // let variable :string = "xxxx";

            
            _lexer.NextTokenIs(TokenKind.Let);

            _lexer.LookAheadAndSkip(TokenKind.Ignore);

            string variableName = _lexer.ScanContentBeforeToken(TokenKind.Colon).ReplaceIgnore();
            _lexer.NextTokenIs(TokenKind.Colon);

            // : 类型 =


            string type = _lexer.ScanContentBeforeToken(TokenKind.Equal).ReplaceIgnore();
            _lexer.NextTokenIs(TokenKind.Equal);
            
            

            var variable = new Variable() {
                Name = variableName,
            };
            if (type == TokenMapper.Map[TokenKind.String])
            {
                variable.Type = VariableType.String;
            }
            else if (type == TokenMapper.Map[TokenKind.Number])
            {
                variable.Type = VariableType.Number;
            }
            else if (type == TokenMapper.Map[TokenKind.Bool])
            {
                variable.Type = VariableType.Bool;
            }
            else
            {
                throw new NotImplementedException("未知的类型");
            }

            // 解析求值语句
            var evalStatement = ParseEvaluationStatement();


            variable.EvalStatement = evalStatement;

            // 吃掉最后行位的分号
            _lexer.NextTokenIs(TokenKind.Semicolon);
            return variable;
        }


        /// <summary>
        /// 解析求值语句
        /// </summary>
        /// <returns></returns>
        public EvaluationStatement ParseEvaluationStatement()
        {
            throw new NotImplementedException();
            
        }



    }
}
