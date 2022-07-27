using Plg.Compiler.AST;
using Plg.Compiler.AST.Expressions;
using Plg.Compiler.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlgCompiler.Tests.ParserTests
{
    public class ParserStatementTests
    {

        [Test]
        public void ParseAssignment()
        {
            {
                var scope = Scope.CreateTopScope();

                Parser parser = new Parser("let numberTest:number = 1;");
                var variable = parser.ParseAssignment(scope);

                Assert.IsTrue(variable.Name == "numberTest");
                Assert.IsTrue(variable.Type == VariableType.Number);
                Assert.IsTrue(variable.Expression.Items.Any(t => t.Type == ExpressionItemType.Number && t.Value == "1"));

            }

            {
                var scope = Scope.CreateTopScope();

                Parser parser = new Parser("let stringTest:string = \"114514\";");
                var variable = parser.ParseAssignment(scope);

                Assert.IsTrue(variable.Name == "stringTest");
                Assert.IsTrue(variable.Type == VariableType.String);
                Assert.IsTrue(variable.Expression.Items.Any(t => t.Type == ExpressionItemType.String && t.Value == "114514"));
            }

            {
                var scope = Scope.CreateTopScope();

                Parser parser = new Parser("let boolTest:bool =false;");
                var variable = parser.ParseAssignment(scope);

                Assert.IsTrue(variable.Name == "boolTest");
                Assert.IsTrue(variable.Type == VariableType.Bool);
                Assert.IsTrue(variable.Expression.Items.Any(t => t.Type == ExpressionItemType.Bool && t.Value == "false"));
            }

        }
        
        [Test]
        public void ParseAssignmentMany()
        {
            {
                var scope = Scope.CreateTopScope();

                Parser parser = new Parser("let numberTest:number = 1 + (2 + 3) * 5 / 4;");
                var variable = parser.ParseAssignment(scope);

                Assert.IsTrue(variable.Name == "numberTest");
                Assert.IsTrue(variable.Type == VariableType.Number);
                string exp = variable.Expression.ToString();
                Console.WriteLine();

            }
        }
    }
}
