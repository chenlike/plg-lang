using Plg.Compiler.AST;
using Plg.Compiler.AST.Commands;
using Plg.Compiler.AST.Expressions;
using Plg.Compiler.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlgCompiler.Tests.ParserTests
{
    public class ParserLetTests
    {

        [Test]
        public void ParseAssignment()
        {
            {
                var scope = Scope.CreateTopScope();

                Parser parser = new Parser("let numberTest:number = 1;");
                var variable = parser.ParseDefineVariaible(scope).Variable;

                Assert.IsTrue(variable.Name == "numberTest");
                Assert.IsTrue(variable.Type == VariableType.Number);
                Assert.IsTrue(variable.Expression.Items.Any(t => t.Type == ExpressionItemType.Number && t.Value == "1"));

            }

            {
                var scope = Scope.CreateTopScope();

                Parser parser = new Parser("let stringTest:string = \"114514\";");
                var variable = parser.ParseDefineVariaible(scope).Variable;

                Assert.IsTrue(variable.Name == "stringTest");
                Assert.IsTrue(variable.Type == VariableType.String);
                Assert.IsTrue(variable.Expression.Items.Any(t => t.Type == ExpressionItemType.String && t.Value == "114514"));
            }

            {
                var scope = Scope.CreateTopScope();

                Parser parser = new Parser("let boolTest:bool =false;");
                var variable = parser.ParseDefineVariaible(scope).Variable;

                Assert.IsTrue(variable.Name == "boolTest");
                Assert.IsTrue(variable.Type == VariableType.Bool);
                Assert.IsTrue(variable.Expression.Items.Any(t => t.Type == ExpressionItemType.Bool && t.Value == "false"));
            }

        }
        
        [Test]
        public void ReversePolishNotation()
        {
            {
                var scope = Scope.CreateTopScope();

                Parser parser = new Parser("let numberTest:number = a+b*c+(d*e+f)*g;");
                var variable = parser.ParseDefineVariaible(scope).Variable;

                Assert.IsTrue(variable.Name == "numberTest");
                Assert.IsTrue(variable.Type == VariableType.Number);
                string exp = variable.Expression.ToString();
                Console.WriteLine();

                Assert.IsTrue(variable.Expression.Items[0].Value == "a");
                Assert.IsTrue(variable.Expression.Items[1].Value == "b");
                Assert.IsTrue(variable.Expression.Items[2].Value == "c");
                Assert.IsTrue(variable.Expression.Items[3].Token.Kind ==  TokenKind.Mul);
                Assert.IsTrue(variable.Expression.Items[4].Token.Kind == TokenKind.Add);
                Assert.IsTrue(variable.Expression.Items[5].Value == "d");
                Assert.IsTrue(variable.Expression.Items[6].Value == "e");
                Assert.IsTrue(variable.Expression.Items[7].Token.Kind == TokenKind.Mul);
                Assert.IsTrue(variable.Expression.Items[8].Value == "f");
                Assert.IsTrue(variable.Expression.Items[9].Token.Kind == TokenKind.Add);
                Assert.IsTrue(variable.Expression.Items[10].Value == "g");
                Assert.IsTrue(variable.Expression.Items[11].Token.Kind == TokenKind.Mul);
                Assert.IsTrue(variable.Expression.Items[12].Token.Kind == TokenKind.Add);

                // abc*+de*f+g*+


                // https://www.cnblogs.com/wkfvawl/p/12864789.html

            }
        }



    }
}
