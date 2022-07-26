using Plg.Compiler.AST;
using Plg.Compiler.Lexer;

namespace Plg.Compiler
{
    public partial class Parser
    {

        Tokenizer _tokenizer;
        public Parser(string sourceCode)
        {
            _tokenizer = new Tokenizer(sourceCode);
        }




        public void Parse()
        {

            while (_tokenizer.LookAhead().Kind != TokenKind.EOF)
            {

                var token = _tokenizer.LookAheadAndSkip(TokenKind.Ignore).Kind;
                switch (token)
                {
                    case TokenKind.Let:
                        ParseAssignment();
                        break;
                    case TokenKind.If:
                        break;
                }
                
            }
        }



        
        /// <summary>
        /// 声明语句
        /// </summary>
        public void ParseAssignment()
        {
            /*
             let aa:string = "123";
             */
            _tokenizer.LookAheadAndSkip(TokenKind.Ignore);
            _tokenizer.NextTokenIs(TokenKind.Let);
            _tokenizer.NextTokenIs(TokenKind.Ignore);

            // 解析变量名
            var nameToken = _tokenizer.NextTokenIs(TokenKind.Name);
            _tokenizer.NextTokenIs(TokenKind.Ignore);


            Variable variable = new Variable()
            {
                Name = nameToken.Value
            };

            
            switch (_tokenizer.LookAhead().Kind)
            {
                case TokenKind.TypeString:
                    _tokenizer.NextTokenIs(TokenKind.TypeString);
                    variable.Type = VariableType.String;
                    break;
                case TokenKind.TypeNumber:
                    _tokenizer.NextTokenIs(TokenKind.TypeNumber);
                    variable.Type = VariableType.Number;
                    break;
                case TokenKind.TypeBool:
                    _tokenizer.NextTokenIs(TokenKind.TypeBool);
                    variable.Type = VariableType.Bool;
                    break;
            }

            _tokenizer.LookAheadAndSkip(TokenKind.Ignore);
            _tokenizer.NextTokenIs(TokenKind.Equal);
            _tokenizer.LookAheadAndSkip(TokenKind.Ignore);

        }


    }
    
}