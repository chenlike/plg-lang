using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlgCompiler.Tests.ParserTests
{
    public class ParserForTests
    {


        [Test]
        public void ParseFor()
        {
            {
                var scope = Scope.CreateScope();

                Parser parser = new Parser(@" 
for(let i:number=0;i<10;i++)
{
    print(i);
}

");
                var forCmd = parser.ParseFor(scope);


                Assert.IsTrue(forCmd.Define.Variable.Name == "i");
                Assert.IsTrue(forCmd.Define.Variable.Type == VariableType.Number);
                Assert.IsTrue(forCmd.Define.Variable.Expression.Items[0].Value == "0");


                Assert.IsTrue(forCmd.Condition.Items[0].Value == "i");
                Assert.IsTrue(forCmd.Condition.Items[1].Value == "10");
                Assert.IsTrue(forCmd.Condition.Items[2].Value == "<");

                var incrementCmd = forCmd.Increment as VariableAssignCommand;
                Assert.IsTrue(incrementCmd.LeftVariableName == "i");

                Assert.IsTrue(incrementCmd.Operator == TokenKind.Increase);


            }

        }

        [Test]
        public void ParseBreak()
        {
            {
                var scope = Scope.CreateScope();

                Parser parser = new Parser(@" break; ");
                var cmd = parser.ParseBreak(scope);
                Assert.IsTrue(cmd.TokenKind == TokenKind.Break);
            }

            {
                var scope = Scope.CreateScope();

                Parser parser = new Parser(@" continue; ");
                var cmd = parser.ParseBreak(scope);
                Assert.IsTrue(cmd.TokenKind == TokenKind.Continue);
            }

            {
                var scope = Scope.CreateScope();

                Parser parser = new Parser(@" return; ");
                var cmd = parser.ParseBreak(scope);
                Assert.IsTrue(cmd.TokenKind == TokenKind.Return && cmd.ReturnValues.Count == 0);
            }

            {


                var scope = Scope.CreateScope();

                Parser parser = new Parser(" return \"abc\",123,aaass,a+2; ");
                var cmd = parser.ParseBreak(scope);
                Assert.IsTrue(cmd.TokenKind == TokenKind.Return && cmd.ReturnValues.Count == 4);

                var values = cmd.ReturnValues;

                Assert.IsTrue(values[0].Items[0].Value == "abc");
                Assert.IsTrue(values[1].Items[0].Value == "123");
                Assert.IsTrue(values[2].Items[0].Value == "aaass");
                Assert.IsTrue(values[3].Items[0].Value == "a");
                Assert.IsTrue(values[3].Items[1].Value == "2");
                Assert.IsTrue(values[3].Items[2].Value == "+");

            }
            

        }
    }
}
